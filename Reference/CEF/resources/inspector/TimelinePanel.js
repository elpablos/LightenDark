WebInspector.CountersGraph=function(delegate,model)
{WebInspector.SplitView.call(this,true,false);this.element.id="memory-graphs-container";this._delegate=delegate;this._model=model;this._calculator=new WebInspector.TimelineCalculator(this._model);this._graphsContainer=this.mainElement();this._createCurrentValuesBar();this._canvasView=new WebInspector.VBoxWithResizeCallback(this._resize.bind(this));this._canvasView.show(this._graphsContainer);this._canvasContainer=this._canvasView.element;this._canvasContainer.id="memory-graphs-canvas-container";this._canvas=this._canvasContainer.createChild("canvas");this._canvas.id="memory-counters-graph";this._canvasContainer.addEventListener("mouseover",this._onMouseMove.bind(this),true);this._canvasContainer.addEventListener("mousemove",this._onMouseMove.bind(this),true);this._canvasContainer.addEventListener("mouseout",this._onMouseOut.bind(this),true);this._canvasContainer.addEventListener("click",this._onClick.bind(this),true);this._timelineGrid=new WebInspector.TimelineGrid();this._canvasContainer.appendChild(this._timelineGrid.dividersElement);this.sidebarElement().createChild("div","sidebar-tree sidebar-tree-section").textContent=WebInspector.UIString("COUNTERS");this._counters=[];this._counterUI=[];}
WebInspector.CountersGraph.prototype={_createCurrentValuesBar:function()
{this._currentValuesBar=this._graphsContainer.createChild("div");this._currentValuesBar.id="counter-values-bar";},createCounter:function(uiName,uiValueTemplate,color)
{var counter=new WebInspector.CountersGraph.Counter();this._counters.push(counter);this._counterUI.push(new WebInspector.CountersGraph.CounterUI(this,uiName,uiValueTemplate,color,counter));return counter;},reset:function()
{for(var i=0;i<this._counters.length;++i){this._counters[i].reset();this._counterUI[i].reset();}
this.refresh();},_resize:function()
{var parentElement=this._canvas.parentElement;this._canvas.width=parentElement.clientWidth;this._canvas.height=parentElement.clientHeight;var timelinePaddingLeft=15;this._calculator.setDisplayWindow(timelinePaddingLeft,this._canvas.width);this.refresh();},setWindowTimes:function(startTime,endTime)
{this._calculator.setWindow(startTime,endTime);this.scheduleRefresh();},scheduleRefresh:function()
{if(this._refreshTimer)
return;this._refreshTimer=setTimeout(this.refresh.bind(this),300);},draw:function()
{for(var i=0;i<this._counters.length;++i){this._counters[i]._calculateVisibleIndexes(this._calculator);this._counters[i]._calculateXValues(this._canvas.width);}
this._clear();this._setVerticalClip(10,this._canvas.height-20);for(var i=0;i<this._counterUI.length;i++)
this._drawGraph(this._counterUI[i]);},_onClick:function(event)
{var x=event.x-this._canvasContainer.totalOffsetLeft();var minDistance=Infinity;var bestTime;for(var i=0;i<this._counterUI.length;++i){var counterUI=this._counterUI[i];if(!counterUI.counter.times.length)
continue;var index=counterUI._recordIndexAt(x);var distance=Math.abs(x-counterUI.counter.x[index]);if(distance<minDistance){minDistance=distance;bestTime=counterUI.counter.times[index];}}
if(bestTime!==undefined)
this._revealRecordAt(bestTime);},_revealRecordAt:function(time)
{var recordToReveal;function findRecordToReveal(record)
{if(record.startTime<=time&&time<=record.endTime){recordToReveal=record;return true;}
if(!recordToReveal||record.endTime<time&&recordToReveal.endTime<record.endTime)
recordToReveal=record;return false;}
this._model.forAllRecords(null,findRecordToReveal);this._delegate.selectRecord(recordToReveal);},_onMouseOut:function(event)
{delete this._markerXPosition;this._clearCurrentValueAndMarker();},_clearCurrentValueAndMarker:function()
{for(var i=0;i<this._counterUI.length;i++)
this._counterUI[i]._clearCurrentValueAndMarker();},_onMouseMove:function(event)
{var x=event.x-this._canvasContainer.totalOffsetLeft();this._markerXPosition=x;this._refreshCurrentValues();},_refreshCurrentValues:function()
{if(this._markerXPosition===undefined)
return;for(var i=0;i<this._counterUI.length;++i)
this._counterUI[i].updateCurrentValue(this._markerXPosition);},refresh:function()
{delete this._refreshTimer;this._timelineGrid.updateDividers(this._calculator);this.draw();this._refreshCurrentValues();},refreshRecords:function()
{},_setVerticalClip:function(originY,height)
{this._originY=originY;this._clippedHeight=height;},_clear:function()
{var ctx=this._canvas.getContext("2d");ctx.clearRect(0,0,ctx.canvas.width,ctx.canvas.height);},highlightSearchResult:function(record,regex,selectRecord)
{},setSelectedRecord:function(record)
{},_drawGraph:function(counterUI)
{var canvas=this._canvas;var ctx=canvas.getContext("2d");var width=canvas.width;var height=this._clippedHeight;var originY=this._originY;var counter=counterUI.counter;var values=counter.values;if(!values.length)
return;var bounds=counter._calculateBounds();var minValue=bounds.min;var maxValue=bounds.max;counterUI.setRange(minValue,maxValue);if(!counterUI.visible())
return;var yValues=counterUI.graphYValues;yValues.length=this._counters.length;var maxYRange=maxValue-minValue;var yFactor=maxYRange?height/(maxYRange):1;ctx.save();ctx.translate(0.5,0.5);ctx.beginPath();var value=values[counter._minimumIndex];var currentY=Math.round(originY+height-(value-minValue)*yFactor);ctx.moveTo(0,currentY);for(var i=counter._minimumIndex;i<=counter._maximumIndex;i++){var x=Math.round(counter.x[i]);ctx.lineTo(x,currentY);var currentValue=values[i];if(typeof currentValue!=="undefined")
value=currentValue;currentY=Math.round(originY+height-(value-minValue)*yFactor);ctx.lineTo(x,currentY);yValues[i]=currentY;}
ctx.lineTo(width,currentY);ctx.lineWidth=1;ctx.strokeStyle=counterUI.graphColor;ctx.stroke();if(counter._limitValue){var limitLineY=Math.round(originY+height-(counter._limitValue-minValue)*yFactor);ctx.moveTo(0,limitLineY);ctx.lineTo(width,limitLineY);ctx.strokeStyle=counterUI.limitColor;ctx.stroke();}
ctx.closePath();ctx.restore();},__proto__:WebInspector.SplitView.prototype}
WebInspector.CountersGraph.Counter=function()
{this.times=[];this.values=[];}
WebInspector.CountersGraph.Counter.prototype={appendSample:function(time,value)
{if(this.values.length&&this.values.peekLast()===value)
return;this.times.push(time);this.values.push(value);},reset:function()
{this.times=[];this.values=[];},setLimit:function(value)
{this._limitValue=value;},_calculateBounds:function()
{var maxValue;var minValue;for(var i=this._minimumIndex;i<=this._maximumIndex;i++){var value=this.values[i];if(minValue===undefined||value<minValue)
minValue=value;if(maxValue===undefined||value>maxValue)
maxValue=value;}
minValue=minValue||0;maxValue=maxValue||1;if(this._limitValue){if(maxValue>this._limitValue*0.5)
maxValue=Math.max(maxValue,this._limitValue);minValue=Math.min(minValue,this._limitValue);}
return{min:minValue,max:maxValue};},_calculateVisibleIndexes:function(calculator)
{var start=calculator.minimumBoundary();var end=calculator.maximumBoundary();this._minimumIndex=Number.constrain(this.times.upperBound(start)-1,0,this.times.length-1);this._maximumIndex=Number.constrain(this.times.lowerBound(end),0,this.times.length-1);this._minTime=start;this._maxTime=end;},_calculateXValues:function(width)
{if(!this.values.length)
return;var xFactor=width/(this._maxTime-this._minTime);this.x=new Array(this.values.length);this.x[this._minimumIndex]=0;for(var i=this._minimumIndex+1;i<this._maximumIndex;i++)
this.x[i]=xFactor*(this.times[i]-this._minTime);this.x[this._maximumIndex]=width;}}
WebInspector.CountersGraph.CounterUI=function(memoryCountersPane,title,currentValueLabel,graphColor,counter)
{this._memoryCountersPane=memoryCountersPane;this.counter=counter;var container=memoryCountersPane.sidebarElement().createChild("div","memory-counter-sidebar-info");var swatchColor=graphColor;this._swatch=new WebInspector.SwatchCheckbox(WebInspector.UIString(title),swatchColor);this._swatch.addEventListener(WebInspector.SwatchCheckbox.Events.Changed,this._toggleCounterGraph.bind(this));container.appendChild(this._swatch.element);this._range=this._swatch.element.createChild("span");this._value=memoryCountersPane._currentValuesBar.createChild("span","memory-counter-value");this._value.style.color=graphColor;this.graphColor=graphColor;this.limitColor=WebInspector.Color.parse(graphColor).setAlpha(0.3).toString(WebInspector.Color.Format.RGBA);this.graphYValues=[];this._currentValueLabel=currentValueLabel;this._marker=memoryCountersPane._canvasContainer.createChild("div","memory-counter-marker");this._marker.style.backgroundColor=graphColor;this._clearCurrentValueAndMarker();}
WebInspector.CountersGraph.CounterUI.prototype={reset:function()
{this._range.textContent="";},setRange:function(minValue,maxValue)
{this._range.textContent=WebInspector.UIString("[%.0f:%.0f]",minValue,maxValue);},_toggleCounterGraph:function(event)
{this._value.classList.toggle("hidden",!this._swatch.checked);this._memoryCountersPane.refresh();},_recordIndexAt:function(x)
{return this.counter.x.upperBound(x,null,this.counter._minimumIndex+1,this.counter._maximumIndex+1)-1;},updateCurrentValue:function(x)
{if(!this.visible()||!this.counter.values.length)
return;var index=this._recordIndexAt(x);this._value.textContent=WebInspector.UIString(this._currentValueLabel,this.counter.values[index]);var y=this.graphYValues[index];this._marker.style.left=x+"px";this._marker.style.top=y+"px";this._marker.classList.remove("hidden");},_clearCurrentValueAndMarker:function()
{this._value.textContent="";this._marker.classList.add("hidden");},visible:function()
{return this._swatch.checked;}}
WebInspector.SwatchCheckbox=function(title,color)
{this.element=document.createElement("div");this._swatch=this.element.createChild("div","swatch");this.element.createChild("span","title").textContent=title;this._color=color;this.checked=true;this.element.addEventListener("click",this._toggleCheckbox.bind(this),true);}
WebInspector.SwatchCheckbox.Events={Changed:"Changed"}
WebInspector.SwatchCheckbox.prototype={get checked()
{return this._checked;},set checked(v)
{this._checked=v;if(this._checked)
this._swatch.style.backgroundColor=this._color;else
this._swatch.style.backgroundColor="";},_toggleCheckbox:function(event)
{this.checked=!this.checked;this.dispatchEventToListeners(WebInspector.SwatchCheckbox.Events.Changed);},__proto__:WebInspector.Object.prototype};WebInspector.MemoryCountersGraph=function(delegate,model)
{WebInspector.CountersGraph.call(this,delegate,model);this._countersByName={};this._countersByName["documents"]=this.createCounter(WebInspector.UIString("Documents"),WebInspector.UIString("Documents: %d"),"#d00");this._countersByName["nodes"]=this.createCounter(WebInspector.UIString("Nodes"),WebInspector.UIString("Nodes: %d"),"#0a0");this._countersByName["jsEventListeners"]=this.createCounter(WebInspector.UIString("Listeners"),WebInspector.UIString("Listeners: %d"),"#00d");if(WebInspector.experimentsSettings.gpuTimeline.isEnabled())
this._countersByName["gpuMemoryUsedKB"]=this.createCounter(WebInspector.UIString("GPU Memory"),WebInspector.UIString("GPU Memory [KB]: %d"),"#c0c");}
WebInspector.MemoryCountersGraph.prototype={addRecord:function(record)
{function addStatistics(record)
{var counters=record.counters;if(!counters)
return;for(var name in counters){var counter=this._countersByName[name];if(counter)
counter.appendSample(record.endTime||record.startTime,counters[name]);}}
WebInspector.TimelineModel.forAllRecords([record],null,addStatistics.bind(this));this.scheduleRefresh();},refreshRecords:function()
{this.reset();var records=this._model.records();for(var i=0;i<records.length;++i)
this.addRecord(records[i]);},__proto__:WebInspector.CountersGraph.prototype};WebInspector.PieChart=function(totalValue,formatter)
{const shadowOffset=0.04;this.element=document.createElementWithClass("div","pie-chart");var svg=this._createSVGChild(this.element,"svg");svg.setAttribute("width","100%");svg.setAttribute("height",(100*(1+shadowOffset))+"%");this._group=this._createSVGChild(svg,"g");var shadow=this._createSVGChild(this._group,"circle");shadow.setAttribute("r",1);shadow.setAttribute("cy",shadowOffset);shadow.setAttribute("fill","hsl(0,0%,70%)");var background=this._createSVGChild(this._group,"circle");background.setAttribute("r",1);background.setAttribute("fill","hsl(0,0%,92%)");if(totalValue){var totalString=formatter?formatter(totalValue):totalValue;this._totalElement=this.element.createChild("div","pie-chart-foreground");this._totalElement.textContent=totalString;this._totalValue=totalValue;}
this._lastAngle=-Math.PI/2;this.setSize(100);}
WebInspector.PieChart.prototype={setTotal:function(value)
{this._totalValue=value;},setSize:function(value)
{this._group.setAttribute("transform","scale("+(value/2)+") translate(1,1)");var size=value+"px";this.element.style.width=size;this.element.style.height=size;if(this._totalElement)
this._totalElement.style.lineHeight=size;},addSlice:function(value,color)
{var sliceAngle=value/this._totalValue*2*Math.PI;if(!isFinite(sliceAngle))
return;sliceAngle=Math.min(sliceAngle,2*Math.PI*0.9999);var path=this._createSVGChild(this._group,"path");var x1=Math.cos(this._lastAngle);var y1=Math.sin(this._lastAngle);this._lastAngle+=sliceAngle;var x2=Math.cos(this._lastAngle);var y2=Math.sin(this._lastAngle);var largeArc=sliceAngle>Math.PI?1:0;path.setAttribute("d","M0,0 L"+x1+","+y1+" A1,1,0,"+largeArc+",1,"+x2+","+y2+" Z");path.setAttribute("fill",color);},_createSVGChild:function(parent,childType)
{var child=document.createElementNS("http://www.w3.org/2000/svg",childType);parent.appendChild(child);return child;}};WebInspector.TimelineModel=function()
{this._filters=[];this._bindings=new WebInspector.TimelineModel.InterRecordBindings();this.reset();WebInspector.timelineManager.addEventListener(WebInspector.TimelineManager.EventTypes.TimelineEventRecorded,this._onRecordAdded,this);WebInspector.timelineManager.addEventListener(WebInspector.TimelineManager.EventTypes.TimelineStarted,this._onStarted,this);WebInspector.timelineManager.addEventListener(WebInspector.TimelineManager.EventTypes.TimelineStopped,this._onStopped,this);WebInspector.timelineManager.addEventListener(WebInspector.TimelineManager.EventTypes.TimelineProgress,this._onProgress,this);}
WebInspector.TimelineModel.TransferChunkLengthBytes=5000000;WebInspector.TimelineModel.RecordType={Root:"Root",Program:"Program",EventDispatch:"EventDispatch",GPUTask:"GPUTask",RequestMainThreadFrame:"RequestMainThreadFrame",BeginFrame:"BeginFrame",ActivateLayerTree:"ActivateLayerTree",DrawFrame:"DrawFrame",ScheduleStyleRecalculation:"ScheduleStyleRecalculation",RecalculateStyles:"RecalculateStyles",InvalidateLayout:"InvalidateLayout",Layout:"Layout",UpdateLayerTree:"UpdateLayerTree",AutosizeText:"AutosizeText",PaintSetup:"PaintSetup",Paint:"Paint",Rasterize:"Rasterize",ScrollLayer:"ScrollLayer",DecodeImage:"DecodeImage",ResizeImage:"ResizeImage",CompositeLayers:"CompositeLayers",ParseHTML:"ParseHTML",TimerInstall:"TimerInstall",TimerRemove:"TimerRemove",TimerFire:"TimerFire",XHRReadyStateChange:"XHRReadyStateChange",XHRLoad:"XHRLoad",EvaluateScript:"EvaluateScript",MarkLoad:"MarkLoad",MarkDOMContent:"MarkDOMContent",MarkFirstPaint:"MarkFirstPaint",TimeStamp:"TimeStamp",ConsoleTime:"ConsoleTime",ScheduleResourceRequest:"ScheduleResourceRequest",ResourceSendRequest:"ResourceSendRequest",ResourceReceiveResponse:"ResourceReceiveResponse",ResourceReceivedData:"ResourceReceivedData",ResourceFinish:"ResourceFinish",FunctionCall:"FunctionCall",GCEvent:"GCEvent",RequestAnimationFrame:"RequestAnimationFrame",CancelAnimationFrame:"CancelAnimationFrame",FireAnimationFrame:"FireAnimationFrame",WebSocketCreate:"WebSocketCreate",WebSocketSendHandshakeRequest:"WebSocketSendHandshakeRequest",WebSocketReceiveHandshakeResponse:"WebSocketReceiveHandshakeResponse",WebSocketDestroy:"WebSocketDestroy",EmbedderCallback:"EmbedderCallback",}
WebInspector.TimelineModel.Events={RecordAdded:"RecordAdded",RecordsCleared:"RecordsCleared",RecordingStarted:"RecordingStarted",RecordingStopped:"RecordingStopped",RecordingProgress:"RecordingProgress",RecordFilterChanged:"RecordFilterChanged"}
WebInspector.TimelineModel.forAllRecords=function(recordsArray,preOrderCallback,postOrderCallback)
{function processRecords(records,depth)
{for(var i=0;i<records.length;++i){var record=records[i];if(preOrderCallback&&preOrderCallback(record,depth))
return true;if(processRecords(record.children,depth+1))
return true;if(postOrderCallback&&postOrderCallback(record,depth))
return true;}
return false;}
return processRecords(recordsArray,0);}
WebInspector.TimelineModel.prototype={forAllRecords:function(preOrderCallback,postOrderCallback)
{WebInspector.TimelineModel.forAllRecords(this._records,preOrderCallback,postOrderCallback);},addFilter:function(filter)
{this._filters.push(filter);filter._model=this;},forAllFilteredRecords:function(callback)
{function processRecord(record,depth)
{var visible=this.isVisible(record);if(visible){if(callback(record,depth))
return true;}
for(var i=0;i<record.children.length;++i){if(processRecord.call(this,record.children[i],visible?depth+1:depth))
return true;}
return false;}
for(var i=0;i<this._records.length;++i)
processRecord.call(this,this._records[i],0);},isVisible:function(record)
{for(var i=0;i<this._filters.length;++i){if(!this._filters[i].accept(record))
return false;}
return true;},_filterChanged:function()
{this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordFilterChanged);},startRecording:function()
{this._clientInitiatedRecording=true;this.reset();var maxStackFrames=WebInspector.settings.timelineCaptureStacks.get()?30:0;this._bufferEvents=WebInspector.experimentsSettings.timelineNoLiveUpdate.isEnabled();var includeGPUEvents=WebInspector.experimentsSettings.gpuTimeline.isEnabled();var liveEvents=[WebInspector.TimelineModel.RecordType.BeginFrame,WebInspector.TimelineModel.RecordType.DrawFrame,WebInspector.TimelineModel.RecordType.RequestMainThreadFrame,WebInspector.TimelineModel.RecordType.ActivateLayerTree];var includeCounters=true;WebInspector.timelineManager.start(maxStackFrames,this._bufferEvents,liveEvents.join(","),includeCounters,includeGPUEvents,this._fireRecordingStarted.bind(this));},stopRecording:function()
{if(!this._clientInitiatedRecording){WebInspector.timelineManager.start(undefined,undefined,undefined,undefined,undefined,stopTimeline.bind(this));return;}
function stopTimeline()
{WebInspector.timelineManager.stop(this._fireRecordingStopped.bind(this));}
this._clientInitiatedRecording=false;WebInspector.timelineManager.stop(this._fireRecordingStopped.bind(this));},records:function()
{return this._records;},_onRecordAdded:function(event)
{if(this._collectionEnabled)
this._addRecord((event.data));},_onStarted:function(event)
{if(event.data){this._fireRecordingStarted();}},_onStopped:function(event)
{if(event.data){this._fireRecordingStopped(null);}},_onProgress:function(event)
{this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordingProgress,event.data);},_fireRecordingStarted:function()
{this._collectionEnabled=true;this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordingStarted);},_fireRecordingStopped:function(error,events)
{this._bufferEvents=false;this._collectionEnabled=false;if(events&&events.length){this.reset();for(var i=0;i<events.length;++i)
this._addRecord(events[i]);}
this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordingStopped);},bufferEvents:function()
{return this._bufferEvents;},_addRecord:function(payload)
{this._internStrings(payload);this._payloads.push(payload);this._updateBoundaries(payload);var record=this._innerAddRecord(payload,null);this._records.push(record);if(record.type===WebInspector.TimelineModel.RecordType.Program)
this._mainThreadTasks.push(record);if(record.type===WebInspector.TimelineModel.RecordType.GPUTask)
this._gpuThreadTasks.push(record);this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordAdded,record);},_innerAddRecord:function(payload,parentRecord)
{var record=new WebInspector.TimelineModel.Record(this,payload,parentRecord);if(WebInspector.TimelineUIUtils.isEventDivider(record))
this._eventDividerRecords.push(record);for(var i=0;payload.children&&i<payload.children.length;++i)
this._innerAddRecord.call(this,payload.children[i],record);record.calculateAggregatedStats();if(parentRecord)
parentRecord._selfTime-=record.endTime-record.startTime;return record;},loadFromFile:function(file,progress)
{var delegate=new WebInspector.TimelineModelLoadFromFileDelegate(this,progress);var fileReader=this._createFileReader(file,delegate);var loader=new WebInspector.TimelineModelLoader(this,fileReader,progress);fileReader.start(loader);},loadFromURL:function(url,progress)
{var delegate=new WebInspector.TimelineModelLoadFromFileDelegate(this,progress);var urlReader=new WebInspector.ChunkedXHRReader(url,delegate);var loader=new WebInspector.TimelineModelLoader(this,urlReader,progress);urlReader.start(loader);},_createFileReader:function(file,delegate)
{return new WebInspector.ChunkedFileReader(file,WebInspector.TimelineModel.TransferChunkLengthBytes,delegate);},_createFileWriter:function()
{return new WebInspector.FileOutputStream();},saveToFile:function()
{var now=new Date();var fileName="TimelineRawData-"+now.toISO8601Compact()+".json";var stream=this._createFileWriter();function callback(accepted)
{if(!accepted)
return;var saver=new WebInspector.TimelineSaver(stream);saver.save(this._payloads,window.navigator.appVersion);}
stream.open(fileName,callback.bind(this));},reset:function()
{this._records=[];this._payloads=[];this._stringPool={};this._minimumRecordTime=-1;this._maximumRecordTime=-1;this._bindings._reset();this._mainThreadTasks=[];this._gpuThreadTasks=[];this._eventDividerRecords=[];this.dispatchEventToListeners(WebInspector.TimelineModel.Events.RecordsCleared);},minimumRecordTime:function()
{return this._minimumRecordTime;},maximumRecordTime:function()
{return this._maximumRecordTime;},_updateBoundaries:function(record)
{var startTime=record.startTime;var endTime=record.endTime;if(this._minimumRecordTime===-1||startTime<this._minimumRecordTime)
this._minimumRecordTime=startTime;if((this._maximumRecordTime===-1&&endTime)||endTime>this._maximumRecordTime)
this._maximumRecordTime=endTime;},mainThreadTasks:function()
{return this._mainThreadTasks;},gpuThreadTasks:function()
{return this._gpuThreadTasks;},eventDividerRecords:function()
{return this._eventDividerRecords;},_internStrings:function(record)
{for(var name in record){var value=record[name];if(typeof value!=="string")
continue;var interned=this._stringPool[value];if(typeof interned==="string")
record[name]=interned;else
this._stringPool[value]=value;}
var children=record.children;for(var i=0;children&&i<children.length;++i)
this._internStrings(children[i]);},__proto__:WebInspector.Object.prototype}
WebInspector.TimelineModel.InterRecordBindings=function(){this._reset();}
WebInspector.TimelineModel.InterRecordBindings.prototype={_reset:function()
{this._sendRequestRecords={};this._timerRecords={};this._requestAnimationFrameRecords={};this._layoutInvalidateStack={};this._lastScheduleStyleRecalculation={};this._webSocketCreateRecords={};}}
WebInspector.TimelineModel.Record=function(model,record,parentRecord)
{this._model=model;var bindings=this._model._bindings;this._aggregatedStats={};this._record=record;this._children=[];if(parentRecord){this.parent=parentRecord;parentRecord.children.push(this);}
this._selfTime=this.endTime-this.startTime;this._lastChildEndTime=this.endTime;this._startTimeOffset=this.startTime-model.minimumRecordTime();if(record.data){if(record.data["url"])
this.url=record.data["url"];if(record.data["rootNode"])
this._relatedBackendNodeId=record.data["rootNode"];else if(record.data["elementId"])
this._relatedBackendNodeId=record.data["elementId"];if(record.data["scriptName"]){this.scriptName=record.data["scriptName"];this.scriptLine=record.data["scriptLine"];}}
if(parentRecord&&parentRecord.callSiteStackTrace)
this.callSiteStackTrace=parentRecord.callSiteStackTrace;var recordTypes=WebInspector.TimelineModel.RecordType;switch(record.type){case recordTypes.ResourceSendRequest:bindings._sendRequestRecords[record.data["requestId"]]=this;break;case recordTypes.ResourceReceiveResponse:var sendRequestRecord=bindings._sendRequestRecords[record.data["requestId"]];if(sendRequestRecord)
this.url=sendRequestRecord.url;break;case recordTypes.ResourceReceivedData:case recordTypes.ResourceFinish:var sendRequestRecord=bindings._sendRequestRecords[record.data["requestId"]];if(sendRequestRecord)
this.url=sendRequestRecord.url;break;case recordTypes.TimerInstall:this.timeout=record.data["timeout"];this.singleShot=record.data["singleShot"];bindings._timerRecords[record.data["timerId"]]=this;break;case recordTypes.TimerFire:var timerInstalledRecord=bindings._timerRecords[record.data["timerId"]];if(timerInstalledRecord){this.callSiteStackTrace=timerInstalledRecord.stackTrace;this.timeout=timerInstalledRecord.timeout;this.singleShot=timerInstalledRecord.singleShot;}
break;case recordTypes.RequestAnimationFrame:bindings._requestAnimationFrameRecords[record.data["id"]]=this;break;case recordTypes.FireAnimationFrame:var requestAnimationRecord=bindings._requestAnimationFrameRecords[record.data["id"]];if(requestAnimationRecord)
this.callSiteStackTrace=requestAnimationRecord.stackTrace;break;case recordTypes.ConsoleTime:var message=record.data["message"];break;case recordTypes.ScheduleStyleRecalculation:bindings._lastScheduleStyleRecalculation[this.frameId]=this;break;case recordTypes.RecalculateStyles:var scheduleStyleRecalculationRecord=bindings._lastScheduleStyleRecalculation[this.frameId];if(!scheduleStyleRecalculationRecord)
break;this.callSiteStackTrace=scheduleStyleRecalculationRecord.stackTrace;break;case recordTypes.InvalidateLayout:var styleRecalcStack;if(!bindings._layoutInvalidateStack[this.frameId]){if(parentRecord.type===recordTypes.RecalculateStyles)
styleRecalcStack=parentRecord.callSiteStackTrace;}
bindings._layoutInvalidateStack[this.frameId]=styleRecalcStack||this.stackTrace;break;case recordTypes.Layout:var layoutInvalidateStack=bindings._layoutInvalidateStack[this.frameId];if(layoutInvalidateStack)
this.callSiteStackTrace=layoutInvalidateStack;if(this.stackTrace)
this.addWarning(WebInspector.UIString("Forced synchronous layout is a possible performance bottleneck."));bindings._layoutInvalidateStack[this.frameId]=null;this.highlightQuad=record.data.root||WebInspector.TimelineModel._quadFromRectData(record.data);this._relatedBackendNodeId=record.data["rootNode"];break;case recordTypes.AutosizeText:if(record.data.needsRelayout&&parentRecord.type===recordTypes.Layout)
parentRecord.addWarning(WebInspector.UIString("Layout required two passes due to text autosizing, consider setting viewport."));break;case recordTypes.Paint:this.highlightQuad=record.data.clip||WebInspector.TimelineModel._quadFromRectData(record.data);break;case recordTypes.WebSocketCreate:this.webSocketURL=record.data["url"];if(typeof record.data["webSocketProtocol"]!=="undefined")
this.webSocketProtocol=record.data["webSocketProtocol"];bindings._webSocketCreateRecords[record.data["identifier"]]=this;break;case recordTypes.WebSocketSendHandshakeRequest:case recordTypes.WebSocketReceiveHandshakeResponse:case recordTypes.WebSocketDestroy:var webSocketCreateRecord=bindings._webSocketCreateRecords[record.data["identifier"]];if(webSocketCreateRecord){this.webSocketURL=webSocketCreateRecord.webSocketURL;if(typeof webSocketCreateRecord.webSocketProtocol!=="undefined")
this.webSocketProtocol=webSocketCreateRecord.webSocketProtocol;}
break;case recordTypes.EmbedderCallback:this.embedderCallbackName=record.data["callbackName"];break;}}
WebInspector.TimelineModel.Record.prototype={get lastChildEndTime()
{return this._lastChildEndTime;},set lastChildEndTime(time)
{this._lastChildEndTime=time;},get selfTime()
{return this._selfTime;},get cpuTime()
{return this._cpuTime;},isRoot:function()
{return this.type===WebInspector.TimelineModel.RecordType.Root;},get children()
{return this._children;},get category()
{return WebInspector.TimelineUIUtils.categoryForRecord(this);},title:function()
{return WebInspector.TimelineUIUtils.recordTitle(this);},get startTime()
{return this._startTime||this._record.startTime;},set startTime(startTime)
{this._startTime=startTime;},get thread()
{return this._record.thread;},get startTimeOffset()
{return this._startTimeOffset;},get endTime()
{return this._endTime||this._record.endTime||this._record.startTime;},set endTime(endTime)
{this._endTime=endTime;},get data()
{return this._record.data;},get type()
{return this._record.type;},get frameId()
{return this._record.frameId||"";},get usedHeapSizeDelta()
{return this._record.usedHeapSizeDelta||0;},get jsHeapSizeUsed()
{return this._record.counters?this._record.counters.jsHeapSizeUsed||0:0;},get counters()
{return this._record.counters;},get stackTrace()
{if(this._record.stackTrace&&this._record.stackTrace.length)
return this._record.stackTrace;return null;},getUserObject:function(key)
{if(!this._userObjects)
return null;return this._userObjects.get(key);},setUserObject:function(key,value)
{if(!this._userObjects)
this._userObjects=new StringMap();this._userObjects.put(key,value);},relatedBackendNodeId:function()
{return this._relatedBackendNodeId;},calculateAggregatedStats:function()
{this._aggregatedStats={};this._cpuTime=this._selfTime;for(var index=this._children.length;index;--index){var child=this._children[index-1];for(var category in child._aggregatedStats)
this._aggregatedStats[category]=(this._aggregatedStats[category]||0)+child._aggregatedStats[category];}
for(var category in this._aggregatedStats)
this._cpuTime+=this._aggregatedStats[category];this._aggregatedStats[this.category.name]=(this._aggregatedStats[this.category.name]||0)+this._selfTime;},get aggregatedStats()
{return this._aggregatedStats;},addWarning:function(message)
{if(this._warnings)
this._warnings.push(message);else{this._warnings=[message];for(var parent=this.parent;parent&&!parent._childHasWarnings;parent=parent.parent)
parent._childHasWarnings=true;}},warnings:function()
{return this._warnings;},childHasWarnings:function()
{return!!this._childHasWarnings;},testContentMatching:function(regExp)
{var tokens=[this.title()];for(var key in this._record.data)
tokens.push(this._record.data[key])
return regExp.test(tokens.join("|"));}}
WebInspector.TimelineModel.Filter=function()
{this._model;}
WebInspector.TimelineModel.Filter.prototype={accept:function(record)
{return true;},notifyFilterChanged:function()
{this._model._filterChanged();}}
WebInspector.TimelineModelLoader=function(model,reader,progress)
{this._model=model;this._reader=reader;this._progress=progress;this._buffer="";this._firstChunk=true;}
WebInspector.TimelineModelLoader.prototype={write:function(chunk)
{var data=this._buffer+chunk;var lastIndex=0;var index;do{index=lastIndex;lastIndex=WebInspector.TextUtils.findBalancedCurlyBrackets(data,index);}while(lastIndex!==-1)
var json=data.slice(0,index)+"]";this._buffer=data.slice(index);if(!index)
return;if(!this._firstChunk)
json="[0"+json;var items;try{items=(JSON.parse(json));}catch(e){WebInspector.console.showErrorMessage("Malformed timeline data.");this._model.reset();this._reader.cancel();this._progress.done();return;}
if(this._firstChunk){this._version=items[0];this._firstChunk=false;this._model.reset();}
for(var i=1,size=items.length;i<size;++i)
this._model._addRecord(items[i]);},close:function(){}}
WebInspector.TimelineModelLoadFromFileDelegate=function(model,progress)
{this._model=model;this._progress=progress;}
WebInspector.TimelineModelLoadFromFileDelegate.prototype={onTransferStarted:function()
{this._progress.setTitle(WebInspector.UIString("Loading\u2026"));},onChunkTransferred:function(reader)
{if(this._progress.isCanceled()){reader.cancel();this._progress.done();this._model.reset();return;}
var totalSize=reader.fileSize();if(totalSize){this._progress.setTotalWork(totalSize);this._progress.setWorked(reader.loadedSize());}},onTransferFinished:function()
{this._progress.done();},onError:function(reader,event)
{this._progress.done();this._model.reset();switch(event.target.error.code){case FileError.NOT_FOUND_ERR:WebInspector.console.showErrorMessage(WebInspector.UIString("File \"%s\" not found.",reader.fileName()));break;case FileError.NOT_READABLE_ERR:WebInspector.console.showErrorMessage(WebInspector.UIString("File \"%s\" is not readable",reader.fileName()));break;case FileError.ABORT_ERR:break;default:WebInspector.console.showErrorMessage(WebInspector.UIString("An error occurred while reading the file \"%s\"",reader.fileName()));}}}
WebInspector.TimelineSaver=function(stream)
{this._stream=stream;}
WebInspector.TimelineSaver.prototype={save:function(payloads,version)
{this._payloads=payloads;this._recordIndex=0;this._prologue="["+JSON.stringify(version);this._writeNextChunk(this._stream);},_writeNextChunk:function(stream)
{const separator=",\n";var data=[];var length=0;if(this._prologue){data.push(this._prologue);length+=this._prologue.length;delete this._prologue;}else{if(this._recordIndex===this._payloads.length){stream.close();return;}
data.push("");}
while(this._recordIndex<this._payloads.length){var item=JSON.stringify(this._payloads[this._recordIndex]);var itemLength=item.length+separator.length;if(length+itemLength>WebInspector.TimelineModel.TransferChunkLengthBytes)
break;length+=itemLength;data.push(item);++this._recordIndex;}
if(this._recordIndex===this._payloads.length)
data.push(data.pop()+"]");stream.write(data.join(separator),this._writeNextChunk.bind(this));}}
WebInspector.TimelineMergingRecordBuffer=function()
{this._backgroundRecordsBuffer=[];}
WebInspector.TimelineMergingRecordBuffer.prototype={process:function(thread,records)
{if(thread){this._backgroundRecordsBuffer=this._backgroundRecordsBuffer.concat(records);return[];}
function recordTimestampComparator(a,b)
{return a.startTime<b.startTime?-1:1;}
var result=this._backgroundRecordsBuffer.mergeOrdered(records,recordTimestampComparator);this._backgroundRecordsBuffer=[];return result;}}
WebInspector.TimelineModel._quadFromRectData=function(data)
{if(typeof data["x"]==="undefined"||typeof data["y"]==="undefined")
return null;var x0=data["x"];var x1=data["x"]+data["width"];var y0=data["y"];var y1=data["y"]+data["height"];return[x0,y0,x1,y0,x1,y1,x0,y1];};WebInspector.TimelineOverviewPane=function(model)
{WebInspector.VBox.call(this);this.element.id="timeline-overview-pane";this._eventDividers=[];this._model=model;this._overviewGrid=new WebInspector.OverviewGrid("timeline");this.element.appendChild(this._overviewGrid.element);this._overviewCalculator=new WebInspector.TimelineOverviewCalculator();model.addEventListener(WebInspector.TimelineModel.Events.RecordsCleared,this._reset,this);this._overviewGrid.addEventListener(WebInspector.OverviewGrid.Events.WindowChanged,this._onWindowChanged,this);}
WebInspector.TimelineOverviewPane.Events={WindowChanged:"WindowChanged"};WebInspector.TimelineOverviewPane.prototype={wasShown:function()
{this._update();},onResize:function()
{this._update();},setOverviewControl:function(overviewControl)
{if(this._overviewControl===overviewControl)
return;var windowTimes=null;if(this._overviewControl){windowTimes=this._overviewControl.windowTimes(this._overviewGrid.windowLeft(),this._overviewGrid.windowRight());this._overviewControl.detach();}
this._overviewControl=overviewControl;this._overviewControl.show(this._overviewGrid.element);this._update();if(windowTimes)
this.requestWindowTimes(windowTimes.startTime,windowTimes.endTime);},_update:function()
{delete this._refreshTimeout;this._overviewCalculator._setWindow(this._model.minimumRecordTime(),this._model.maximumRecordTime());this._overviewCalculator._setDisplayWindow(0,this._overviewGrid.clientWidth());if(this._overviewControl)
this._overviewControl.update();this._overviewGrid.updateDividers(this._overviewCalculator);this._updateEventDividers();this._updateWindow();},_updateEventDividers:function()
{var records=this._eventDividers;this._overviewGrid.removeEventDividers();var dividers=[];for(var i=0;i<records.length;++i){var record=records[i];var positions=this._overviewCalculator.computeBarGraphPercentages(record);var dividerPosition=Math.round(positions.start*10);if(dividers[dividerPosition])
continue;var divider=WebInspector.TimelineUIUtils.createEventDivider(record.type);divider.style.left=positions.start+"%";dividers[dividerPosition]=divider;}
this._overviewGrid.addEventDividers(dividers);},addRecord:function(record)
{var eventDividers=this._eventDividers;function addEventDividers(record)
{if(WebInspector.TimelineUIUtils.isEventDivider(record))
eventDividers.push(record);}
WebInspector.TimelineModel.forAllRecords([record],addEventDividers);this._scheduleRefresh();},_reset:function()
{this._overviewCalculator.reset();this._overviewGrid.reset();this._overviewGrid.setResizeEnabled(false);this._eventDividers=[];this._overviewGrid.updateDividers(this._overviewCalculator);if(this._overviewControl)
this._overviewControl.reset();this._update();},_onWindowChanged:function(event)
{if(this._muteOnWindowChanged)
return;var windowTimes=this._overviewControl.windowTimes(this._overviewGrid.windowLeft(),this._overviewGrid.windowRight());this._windowStartTime=windowTimes.startTime;this._windowEndTime=windowTimes.endTime;this.dispatchEventToListeners(WebInspector.TimelineOverviewPane.Events.WindowChanged,windowTimes);},requestWindowTimes:function(startTime,endTime)
{if(startTime===this._windowStartTime&&endTime===this._windowEndTime)
return;this._windowStartTime=startTime;this._windowEndTime=endTime;this._updateWindow();this.dispatchEventToListeners(WebInspector.TimelineOverviewPane.Events.WindowChanged,{startTime:startTime,endTime:endTime});},_updateWindow:function()
{var windowBoundaries=this._overviewControl.windowBoundaries(this._windowStartTime,this._windowEndTime);this._muteOnWindowChanged=true;this._overviewGrid.setWindow(windowBoundaries.left,windowBoundaries.right);this._overviewGrid.setResizeEnabled(!!this._model.records().length);this._muteOnWindowChanged=false;},_scheduleRefresh:function()
{if(this._refreshTimeout)
return;if(!this.isShowing())
return;this._refreshTimeout=setTimeout(this._update.bind(this),300);},__proto__:WebInspector.VBox.prototype}
WebInspector.TimelineOverviewCalculator=function()
{}
WebInspector.TimelineOverviewCalculator.prototype={paddingLeft:function()
{return this._paddingLeft;},computePosition:function(time)
{return(time-this._minimumBoundary)/this.boundarySpan()*this._workingArea+this._paddingLeft;},computeBarGraphPercentages:function(record)
{var start=(record.startTime-this._minimumBoundary)/this.boundarySpan()*100;var end=(record.endTime-this._minimumBoundary)/this.boundarySpan()*100;return{start:start,end:end};},_setWindow:function(minimumRecordTime,maximumRecordTime)
{this._minimumBoundary=minimumRecordTime;this._maximumBoundary=maximumRecordTime;},_setDisplayWindow:function(paddingLeft,clientWidth)
{this._workingArea=clientWidth-paddingLeft;this._paddingLeft=paddingLeft;},reset:function()
{this._setWindow(0,1000);},formatTime:function(value,precision)
{return Number.preciseMillisToString(value-this.zeroTime(),precision);},maximumBoundary:function()
{return this._maximumBoundary;},minimumBoundary:function()
{return this._minimumBoundary;},zeroTime:function()
{return this._minimumBoundary;},boundarySpan:function()
{return this._maximumBoundary-this._minimumBoundary;}}
WebInspector.TimelineOverview=function(model)
{}
WebInspector.TimelineOverview.prototype={show:function(parentElement,insertBefore){},update:function(){},reset:function(){},windowTimes:function(windowLeft,windowRight){},windowBoundaries:function(startTime,endTime){}}
WebInspector.TimelineOverviewBase=function(model)
{WebInspector.VBox.call(this);this._model=model;this._canvas=this.element.createChild("canvas","fill");this._context=this._canvas.getContext("2d");}
WebInspector.TimelineOverviewBase.prototype={update:function()
{this.resetCanvas();},reset:function()
{},timelineStarted:function(){},timelineStopped:function(){},windowTimes:function(windowLeft,windowRight)
{var absoluteMin=this._model.minimumRecordTime();var timeSpan=this._model.maximumRecordTime()-absoluteMin;return{startTime:absoluteMin+timeSpan*windowLeft,endTime:absoluteMin+timeSpan*windowRight};},windowBoundaries:function(startTime,endTime)
{var absoluteMin=this._model.minimumRecordTime();var timeSpan=this._model.maximumRecordTime()-absoluteMin;var haveRecords=absoluteMin>=0;return{left:haveRecords&&startTime?Math.min((startTime-absoluteMin)/timeSpan,1):0,right:haveRecords&&endTime<Infinity?(endTime-absoluteMin)/timeSpan:1}},resetCanvas:function()
{this._canvas.width=this.element.clientWidth*window.devicePixelRatio;this._canvas.height=this.element.clientHeight*window.devicePixelRatio;},__proto__:WebInspector.VBox.prototype};WebInspector.TimelinePresentationModel=function(model)
{this._model=model;this._filters=[];this._recordToPresentationRecord=new Map();this.reset();}
WebInspector.TimelinePresentationModel._coalescingRecords={};WebInspector.TimelinePresentationModel._coalescingRecords[WebInspector.TimelineModel.RecordType.Layout]=1;WebInspector.TimelinePresentationModel._coalescingRecords[WebInspector.TimelineModel.RecordType.Paint]=1;WebInspector.TimelinePresentationModel._coalescingRecords[WebInspector.TimelineModel.RecordType.Rasterize]=1;WebInspector.TimelinePresentationModel._coalescingRecords[WebInspector.TimelineModel.RecordType.DecodeImage]=1;WebInspector.TimelinePresentationModel._coalescingRecords[WebInspector.TimelineModel.RecordType.ResizeImage]=1;WebInspector.TimelinePresentationModel.prototype={setWindowTimes:function(startTime,endTime)
{this._windowStartTime=startTime;this._windowEndTime=endTime;},toPresentationRecord:function(record)
{return record?this._recordToPresentationRecord.get(record)||null:null;},rootRecord:function()
{return this._rootRecord;},reset:function()
{this._recordToPresentationRecord.clear();var rootPayload={type:WebInspector.TimelineModel.RecordType.Root};var rootRecord=new WebInspector.TimelineModel.Record(this._model,(rootPayload),null);this._rootRecord=new WebInspector.TimelinePresentationModel.Record(rootRecord,null);this._coalescingBuckets={};this._windowStartTime=0;this._windowEndTime=Infinity;},addRecord:function(record)
{var records;if(record.type===WebInspector.TimelineModel.RecordType.Program)
records=record.children;else
records=[record];for(var i=0;i<records.length;++i)
this._innerAddRecord(this._rootRecord,records[i]);},_innerAddRecord:function(parentRecord,record)
{var coalescingBucket;if(parentRecord===this._rootRecord)
coalescingBucket=record.thread?record.type:"mainThread";var coalescedRecord=this._findCoalescedParent(record,parentRecord,coalescingBucket);if(coalescedRecord)
parentRecord=coalescedRecord;var formattedRecord=new WebInspector.TimelinePresentationModel.Record(record,parentRecord);this._recordToPresentationRecord.put(record,formattedRecord);formattedRecord._collapsed=parentRecord===this._rootRecord;if(coalescingBucket)
this._coalescingBuckets[coalescingBucket]=formattedRecord;for(var i=0;record.children&&i<record.children.length;++i)
this._innerAddRecord(formattedRecord,record.children[i]);if(parentRecord._coalesced)
this._updateCoalescingParent(formattedRecord);},_findCoalescedParent:function(record,newParent,bucket)
{const coalescingThresholdMillis=5;var lastRecord=bucket?this._coalescingBuckets[bucket]:newParent._presentationChildren.peekLast();if(lastRecord&&lastRecord._coalesced)
lastRecord=lastRecord._presentationChildren.peekLast();var startTime=record.startTime;var endTime=record.endTime;if(!lastRecord)
return null;if(lastRecord.record().type!==record.type)
return null;if(!WebInspector.TimelinePresentationModel._coalescingRecords[record.type])
return null;if(lastRecord.record().endTime+coalescingThresholdMillis<startTime)
return null;if(endTime+coalescingThresholdMillis<lastRecord.record().startTime)
return null;if(lastRecord.presentationParent()._coalesced)
return lastRecord.presentationParent();return this._replaceWithCoalescedRecord(lastRecord);},_replaceWithCoalescedRecord:function(presentationRecord)
{var record=presentationRecord.record();var rawRecord={type:record.type,startTime:record.startTime,endTime:record.endTime,data:{}};if(record.thread)
rawRecord.thread="aggregated";if(record.type===WebInspector.TimelineModel.RecordType.TimeStamp)
rawRecord.data["message"]=record.data.message;var modelRecord=new WebInspector.TimelineModel.Record(this._model,(rawRecord),null);var coalescedRecord=new WebInspector.TimelinePresentationModel.Record(modelRecord,null);var parent=presentationRecord._presentationParent;coalescedRecord._coalesced=true;coalescedRecord._collapsed=true;coalescedRecord._presentationChildren.push(presentationRecord);presentationRecord._presentationParent=coalescedRecord;if(presentationRecord.hasWarnings()||presentationRecord.childHasWarnings())
coalescedRecord._childHasWarnings=true;coalescedRecord._presentationParent=parent;parent._presentationChildren[parent._presentationChildren.indexOf(presentationRecord)]=coalescedRecord;WebInspector.TimelineUIUtils.aggregateTimeByCategory(modelRecord.aggregatedStats,record.aggregatedStats);return coalescedRecord;},_updateCoalescingParent:function(presentationRecord)
{var record=presentationRecord.record();var parentRecord=presentationRecord._presentationParent.record();WebInspector.TimelineUIUtils.aggregateTimeByCategory(parentRecord.aggregatedStats,record.aggregatedStats);if(parentRecord.startTime>record.startTime)
parentRecord.startTime=record.startTime;if(parentRecord.endTime<record.endTime){parentRecord.endTime=record.endTime;parentRecord.lastChildEndTime=parentRecord.endTime;}},setTextFilter:function(textFilter)
{this._textFilter=textFilter;},invalidateFilteredRecords:function()
{delete this._filteredRecords;},filteredRecords:function()
{if(this._filteredRecords)
return this._filteredRecords;var recordsInWindow=[];var stack=[{children:this._rootRecord._presentationChildren,index:0,parentIsCollapsed:false,parentRecord:{}}];var revealedDepth=0;function revealRecordsInStack(){for(var depth=revealedDepth+1;depth<stack.length;++depth){if(stack[depth-1].parentIsCollapsed){stack[depth].parentRecord._presentationParent._expandable=true;return;}
stack[depth-1].parentRecord._collapsed=false;recordsInWindow.push(stack[depth].parentRecord);stack[depth].windowLengthBeforeChildrenTraversal=recordsInWindow.length;stack[depth].parentIsRevealed=true;revealedDepth=depth;}}
while(stack.length){var entry=stack[stack.length-1];var records=entry.children;if(records&&entry.index<records.length){var record=records[entry.index];++entry.index;var rawRecord=record.record();if(rawRecord.startTime<this._windowEndTime&&rawRecord.endTime>this._windowStartTime){if(this._model.isVisible(rawRecord)){record._presentationParent._expandable=true;if(this._textFilter)
revealRecordsInStack();if(!entry.parentIsCollapsed){recordsInWindow.push(record);revealedDepth=stack.length;entry.parentRecord._collapsed=false;}}}
record._expandable=false;stack.push({children:record._presentationChildren,index:0,parentIsCollapsed:entry.parentIsCollapsed||(record._collapsed&&(!this._textFilter||record._expandedOrCollapsedWhileFiltered)),parentRecord:record,windowLengthBeforeChildrenTraversal:recordsInWindow.length});}else{stack.pop();revealedDepth=Math.min(revealedDepth,stack.length-1);entry.parentRecord._visibleChildrenCount=recordsInWindow.length-entry.windowLengthBeforeChildrenTraversal;}}
this._filteredRecords=recordsInWindow;return recordsInWindow;},__proto__:WebInspector.Object.prototype}
WebInspector.TimelinePresentationModel.Record=function(record,parentRecord)
{this._record=record;this._presentationChildren=[];if(parentRecord){this._presentationParent=parentRecord;parentRecord._presentationChildren.push(this);}
if(this.hasWarnings()){for(var parent=this._presentationParent;parent&&!parent._childHasWarnings;parent=parent._presentationParent)
parent._childHasWarnings=true;}
if(parentRecord&&parentRecord.callSiteStackTrace)
this.callSiteStackTrace=parentRecord.callSiteStackTrace;}
WebInspector.TimelinePresentationModel.Record.prototype={record:function()
{return this._record;},presentationChildren:function()
{return this._presentationChildren;},coalesced:function()
{return this._coalesced;},collapsed:function()
{return this._collapsed;},setCollapsed:function(collapsed)
{this._collapsed=collapsed;this._expandedOrCollapsedWhileFiltered=true;},presentationParent:function()
{return this._presentationParent||null;},visibleChildrenCount:function()
{return this._visibleChildrenCount||0;},expandable:function()
{return!!this._expandable;},hasWarnings:function()
{return!!this._record.warnings();},childHasWarnings:function()
{return this._childHasWarnings;},listRow:function()
{return this._listRow;},setListRow:function(listRow)
{this._listRow=listRow;},graphRow:function()
{return this._graphRow;},setGraphRow:function(graphRow)
{this._graphRow=graphRow;}};WebInspector.TimelineFrameModel=function(model)
{this._model=model;this.reset();var records=model.records();for(var i=0;i<records.length;++i)
this.addRecord(records[i]);}
WebInspector.TimelineFrameModel.Events={FrameAdded:"FrameAdded"}
WebInspector.TimelineFrameModel._mainFrameMarkers=[WebInspector.TimelineModel.RecordType.ScheduleStyleRecalculation,WebInspector.TimelineModel.RecordType.InvalidateLayout,WebInspector.TimelineModel.RecordType.BeginFrame,WebInspector.TimelineModel.RecordType.ScrollLayer];WebInspector.TimelineFrameModel.prototype={frames:function()
{return this._frames;},filteredFrames:function(startTime,endTime)
{function compareStartTime(value,object)
{return value-object.startTime;}
function compareEndTime(value,object)
{return value-object.endTime;}
var frames=this._frames;var firstFrame=insertionIndexForObjectInListSortedByFunction(startTime,frames,compareEndTime);var lastFrame=insertionIndexForObjectInListSortedByFunction(endTime,frames,compareStartTime);return frames.slice(firstFrame,lastFrame);},reset:function()
{this._frames=[];this._lastFrame=null;this._lastLayerTree=null;this._hasThreadedCompositing=false;this._mainFrameCommitted=false;this._mainFrameRequested=false;this._aggregatedMainThreadWork=null;this._mergingBuffer=new WebInspector.TimelineMergingRecordBuffer();},addRecord:function(record)
{var recordTypes=WebInspector.TimelineModel.RecordType;var programRecord=record.type===recordTypes.Program?record:null;if(programRecord){if(!this._aggregatedMainThreadWork&&this._findRecordRecursively(WebInspector.TimelineFrameModel._mainFrameMarkers,programRecord))
this._aggregatedMainThreadWork={};}
var records;if(this._model.bufferEvents())
records=[record];else
records=this._mergingBuffer.process(record.thread,programRecord?record.children||[]:[record]);for(var i=0;i<records.length;++i){if(records[i].thread)
this._addBackgroundRecord(records[i]);else
this._addMainThreadRecord(programRecord,records[i]);}},handleBeginFrame:function(startTime)
{if(!this._lastFrame)
this._startBackgroundFrame(startTime);},handleDrawFrame:function(startTime)
{if(!this._lastFrame){this._startBackgroundFrame(startTime);return;}
if(this._mainFrameCommitted||!this._mainFrameRequested)
this._startBackgroundFrame(startTime);this._mainFrameCommitted=false;},handleActivateLayerTree:function()
{if(!this._lastFrame)
return;this._mainFrameRequested=false;this._mainFrameCommitted=true;this._lastFrame._addTimeForCategories(this._aggregatedMainThreadWorkToAttachToBackgroundFrame);this._aggregatedMainThreadWorkToAttachToBackgroundFrame={};},handleRequestMainThreadFrame:function()
{if(!this._lastFrame)
return;this._mainFrameRequested=true;},_addBackgroundRecord:function(record)
{var recordTypes=WebInspector.TimelineModel.RecordType;if(record.type===recordTypes.BeginFrame)
this.handleBeginFrame(record.startTime);else if(record.type===recordTypes.DrawFrame)
this.handleDrawFrame(record.startTime);else if(record.type===recordTypes.RequestMainThreadFrame)
this.handleRequestMainThreadFrame();else if(record.type===recordTypes.ActivateLayerTree)
this.handleActivateLayerTree();if(this._lastFrame)
this._lastFrame._addTimeFromRecord(record);},_addMainThreadRecord:function(programRecord,record)
{var recordTypes=WebInspector.TimelineModel.RecordType;if(record.type===recordTypes.UpdateLayerTree)
this._lastLayerTree=record.data["layerTree"]||null;if(!this._hasThreadedCompositing){if(record.type===recordTypes.BeginFrame)
this._startMainThreadFrame(record.startTime);if(!this._lastFrame)
return;this._lastFrame._addTimeFromRecord(record);if(programRecord.children[0]===record){this._deriveOtherTime(programRecord,this._lastFrame.timeByCategory);this._lastFrame._updateCpuTime();}
return;}
if(!this._aggregatedMainThreadWork)
return;WebInspector.TimelineUIUtils.aggregateTimeForRecord(this._aggregatedMainThreadWork,record);if(programRecord.children[0]===record)
this._deriveOtherTime(programRecord,this._aggregatedMainThreadWork);if(record.type===recordTypes.CompositeLayers){this._aggregatedMainThreadWorkToAttachToBackgroundFrame=this._aggregatedMainThreadWork;this._aggregatedMainThreadWork=null;}},_deriveOtherTime:function(programRecord,timeByCategory)
{var accounted=0;for(var i=0;i<programRecord.children.length;++i)
accounted+=programRecord.children[i].endTime-programRecord.children[i].startTime;var otherTime=programRecord.endTime-programRecord.startTime-accounted;timeByCategory["other"]=(timeByCategory["other"]||0)+otherTime;},_startBackgroundFrame:function(startTime)
{if(!this._hasThreadedCompositing){this._lastFrame=null;this._hasThreadedCompositing=true;}
if(this._lastFrame)
this._flushFrame(this._lastFrame,startTime);this._lastFrame=new WebInspector.TimelineFrame(startTime,startTime-this._model.minimumRecordTime());},_startMainThreadFrame:function(startTime)
{if(this._lastFrame)
this._flushFrame(this._lastFrame,startTime);this._lastFrame=new WebInspector.TimelineFrame(startTime,startTime-this._model.minimumRecordTime());},_flushFrame:function(frame,endTime)
{frame._setLayerTree(this._lastLayerTree);frame._setEndTime(endTime);this._frames.push(frame);this.dispatchEventToListeners(WebInspector.TimelineFrameModel.Events.FrameAdded,frame);},_findRecordRecursively:function(types,record)
{if(types.indexOf(record.type)>=0)
return record;if(!record.children)
return null;for(var i=0;i<record.children.length;++i){var result=this._findRecordRecursively(types,record.children[i]);if(result)
return result;}
return null;},__proto__:WebInspector.Object.prototype}
WebInspector.FrameStatistics=function(frames)
{this.frameCount=frames.length;this.minDuration=Infinity;this.maxDuration=0;this.timeByCategory={};this.startOffset=frames[0].startTimeOffset;var lastFrame=frames[this.frameCount-1];this.endOffset=lastFrame.startTimeOffset+lastFrame.duration;var totalDuration=0;var sumOfSquares=0;for(var i=0;i<this.frameCount;++i){var duration=frames[i].duration;totalDuration+=duration;sumOfSquares+=duration*duration;this.minDuration=Math.min(this.minDuration,duration);this.maxDuration=Math.max(this.maxDuration,duration);WebInspector.TimelineUIUtils.aggregateTimeByCategory(this.timeByCategory,frames[i].timeByCategory);}
this.average=totalDuration/this.frameCount;var variance=sumOfSquares/this.frameCount-this.average*this.average;this.stddev=Math.sqrt(variance);}
WebInspector.TimelineFrame=function(startTime,startTimeOffset)
{this.startTime=startTime;this.startTimeOffset=startTimeOffset;this.endTime=this.startTime;this.duration=0;this.timeByCategory={};this.cpuTime=0;this.layerTree=null;}
WebInspector.TimelineFrame.prototype={_setEndTime:function(endTime)
{this.endTime=endTime;this.duration=this.endTime-this.startTime;},_setLayerTree:function(layerTree)
{this.layerTree=layerTree;},_addTimeFromRecord:function(record)
{if(!record.endTime)
return;WebInspector.TimelineUIUtils.aggregateTimeForRecord(this.timeByCategory,record);this._updateCpuTime();},_addTimeForCategories:function(timeByCategory)
{WebInspector.TimelineUIUtils.aggregateTimeByCategory(this.timeByCategory,timeByCategory);this._updateCpuTime();},_updateCpuTime:function()
{this.cpuTime=0;for(var key in this.timeByCategory)
this.cpuTime+=this.timeByCategory[key];}};WebInspector.TimelineEventOverview=function(model)
{WebInspector.TimelineOverviewBase.call(this,model);this.element.id="timeline-overview-events";this._fillStyles={};var categories=WebInspector.TimelineUIUtils.categories();for(var category in categories){this._fillStyles[category]=WebInspector.TimelineUIUtils.createFillStyleForCategory(this._context,0,WebInspector.TimelineEventOverview._stripGradientHeight,categories[category]);categories[category].addEventListener(WebInspector.TimelineCategory.Events.VisibilityChanged,this._onCategoryVisibilityChanged,this);}
this._disabledCategoryFillStyle=WebInspector.TimelineUIUtils.createFillStyle(this._context,0,WebInspector.TimelineEventOverview._stripGradientHeight,"hsl(0, 0%, 85%)","hsl(0, 0%, 67%)","hsl(0, 0%, 56%)");this._disabledCategoryBorderStyle="rgb(143, 143, 143)";}
WebInspector.TimelineEventOverview._numberOfStrips=3;WebInspector.TimelineEventOverview._stripGradientHeight=120;WebInspector.TimelineEventOverview.prototype={update:function()
{this.resetCanvas();var stripHeight=Math.round(this._canvas.height/WebInspector.TimelineEventOverview._numberOfStrips);var timeOffset=this._model.minimumRecordTime();var timeSpan=this._model.maximumRecordTime()-timeOffset;var scale=this._canvas.width/timeSpan;var lastBarByGroup=[];this._context.fillStyle="rgba(0, 0, 0, 0.05)";for(var i=1;i<WebInspector.TimelineEventOverview._numberOfStrips;i+=2)
this._context.fillRect(0.5,i*stripHeight+0.5,this._canvas.width,stripHeight);function appendRecord(record)
{if(record.type===WebInspector.TimelineModel.RecordType.BeginFrame)
return;var recordStart=Math.floor((record.startTime-timeOffset)*scale);var recordEnd=Math.ceil((record.endTime-timeOffset)*scale);var category=WebInspector.TimelineUIUtils.categoryForRecord(record);if(category.overviewStripGroupIndex<0)
return;var bar=lastBarByGroup[category.overviewStripGroupIndex];const barsMergeThreshold=2;if(bar&&bar.category===category&&bar.end+barsMergeThreshold>=recordStart){if(recordEnd>bar.end)
bar.end=recordEnd;return;}
if(bar)
this._renderBar(bar.start,bar.end,stripHeight,bar.category);lastBarByGroup[category.overviewStripGroupIndex]={start:recordStart,end:recordEnd,category:category};}
this._model.forAllRecords(appendRecord.bind(this));for(var i=0;i<lastBarByGroup.length;++i){if(lastBarByGroup[i])
this._renderBar(lastBarByGroup[i].start,lastBarByGroup[i].end,stripHeight,lastBarByGroup[i].category);}},_onCategoryVisibilityChanged:function()
{this.update();},_renderBar:function(begin,end,height,category)
{const stripPadding=4*window.devicePixelRatio;const innerStripHeight=height-2*stripPadding;var x=begin+0.5;var y=category.overviewStripGroupIndex*height+stripPadding+0.5;var width=Math.max(end-begin,1);this._context.save();this._context.translate(x,y);this._context.scale(1,innerStripHeight/WebInspector.TimelineEventOverview._stripGradientHeight);this._context.fillStyle=category.hidden?this._disabledCategoryFillStyle:this._fillStyles[category.name];this._context.fillRect(0,0,width,WebInspector.TimelineEventOverview._stripGradientHeight);this._context.strokeStyle=category.hidden?this._disabledCategoryBorderStyle:category.borderColor;this._context.strokeRect(0,0,width,WebInspector.TimelineEventOverview._stripGradientHeight);this._context.restore();},__proto__:WebInspector.TimelineOverviewBase.prototype};WebInspector.TimelineFrameOverview=function(model,frameModel)
{WebInspector.TimelineOverviewBase.call(this,model);this.element.id="timeline-overview-frames";this._frameModel=frameModel;this.reset();this._outerPadding=4*window.devicePixelRatio;this._maxInnerBarWidth=10*window.devicePixelRatio;this._topPadding=6*window.devicePixelRatio;this._actualPadding=5*window.devicePixelRatio;this._actualOuterBarWidth=this._maxInnerBarWidth+this._actualPadding;this._fillStyles={};var categories=WebInspector.TimelineUIUtils.categories();for(var category in categories)
this._fillStyles[category]=WebInspector.TimelineUIUtils.createFillStyleForCategory(this._context,this._maxInnerBarWidth,0,categories[category]);this._frameTopShadeGradient=this._context.createLinearGradient(0,0,0,this._topPadding);this._frameTopShadeGradient.addColorStop(0,"rgba(255, 255, 255, 0.9)");this._frameTopShadeGradient.addColorStop(1,"rgba(255, 255, 255, 0.2)");}
WebInspector.TimelineFrameOverview.prototype={reset:function()
{this._recordsPerBar=1;this._barTimes=[];},update:function()
{this.resetCanvas();this._barTimes=[];const minBarWidth=4*window.devicePixelRatio;var frames=this._frameModel.frames();var framesPerBar=Math.max(1,frames.length*minBarWidth/this._canvas.width);var visibleFrames=this._aggregateFrames(frames,framesPerBar);this._context.save();var scale=(this._canvas.height-this._topPadding)/this._computeTargetFrameLength(visibleFrames);this._renderBars(visibleFrames,scale,this._canvas.height);this._context.fillStyle=this._frameTopShadeGradient;this._context.fillRect(0,0,this._canvas.width,this._topPadding);this._drawFPSMarks(scale,this._canvas.height);this._context.restore();},_aggregateFrames:function(frames,framesPerBar)
{var visibleFrames=[];for(var barNumber=0,currentFrame=0;currentFrame<frames.length;++barNumber){var barStartTime=frames[currentFrame].startTime;var longestFrame=null;var longestDuration=0;for(var lastFrame=Math.min(Math.floor((barNumber+1)*framesPerBar),frames.length);currentFrame<lastFrame;++currentFrame){var duration=frames[currentFrame].duration;if(!longestFrame||longestDuration<duration){longestFrame=frames[currentFrame];longestDuration=duration;}}
var barEndTime=frames[currentFrame-1].endTime;if(longestFrame){visibleFrames.push(longestFrame);this._barTimes.push({startTime:barStartTime,endTime:barEndTime});}}
return visibleFrames;},_computeTargetFrameLength:function(frames)
{var durations=[];for(var i=0;i<frames.length;++i){if(frames[i])
durations.push(frames[i].duration);}
var medianFrameLength=durations.qselect(Math.floor(durations.length/2));const targetFPS=20;var result=1000.0/targetFPS;if(result>=medianFrameLength)
return result;var maxFrameLength=Math.max.apply(Math,durations);return Math.min(medianFrameLength*2,maxFrameLength);},_renderBars:function(frames,scale,windowHeight)
{const maxPadding=5*window.devicePixelRatio;this._actualOuterBarWidth=Math.min((this._canvas.width-2*this._outerPadding)/frames.length,this._maxInnerBarWidth+maxPadding);this._actualPadding=Math.min(Math.floor(this._actualOuterBarWidth/3),maxPadding);var barWidth=this._actualOuterBarWidth-this._actualPadding;for(var i=0;i<frames.length;++i){if(frames[i])
this._renderBar(this._barNumberToScreenPosition(i),barWidth,windowHeight,frames[i],scale);}},_barNumberToScreenPosition:function(n)
{return this._outerPadding+this._actualOuterBarWidth*n;},_drawFPSMarks:function(scale,height)
{const fpsMarks=[30,60];this._context.save();this._context.beginPath();this._context.font=(10*window.devicePixelRatio)+"px "+window.getComputedStyle(this.element,null).getPropertyValue("font-family");this._context.textAlign="right";this._context.textBaseline="alphabetic";const labelPadding=4*window.devicePixelRatio;const baselineHeight=3*window.devicePixelRatio;var lineHeight=12*window.devicePixelRatio;var labelTopMargin=0;var labelOffsetY=0;for(var i=0;i<fpsMarks.length;++i){var fps=fpsMarks[i];var y=height-Math.floor(1000.0/fps*scale)-0.5;var label=WebInspector.UIString("%d\u2009fps",fps);var labelWidth=this._context.measureText(label).width+2*labelPadding;var labelX=this._canvas.width;if(!i&&labelTopMargin<y-lineHeight)
labelOffsetY=-lineHeight;var labelY=y+labelOffsetY;if(labelY<labelTopMargin||labelY+lineHeight>height)
break;this._context.moveTo(0,y);this._context.lineTo(this._canvas.width,y);this._context.fillStyle="rgba(255, 255, 255, 0.5)";this._context.fillRect(labelX-labelWidth,labelY,labelWidth,lineHeight);this._context.fillStyle="black";this._context.fillText(label,labelX-labelPadding,labelY+lineHeight-baselineHeight);labelTopMargin=labelY+lineHeight;}
this._context.strokeStyle="rgba(60, 60, 60, 0.4)";this._context.stroke();this._context.restore();},_renderBar:function(left,width,windowHeight,frame,scale)
{var categories=Object.keys(WebInspector.TimelineUIUtils.categories());var x=Math.floor(left)+0.5;width=Math.floor(width);var totalCPUTime=frame.cpuTime;var normalizedScale=scale;if(totalCPUTime>frame.duration)
normalizedScale*=frame.duration/totalCPUTime;for(var i=0,bottomOffset=windowHeight;i<categories.length;++i){var category=categories[i];var duration=frame.timeByCategory[category];if(!duration)
continue;var height=Math.round(duration*normalizedScale);var y=Math.floor(bottomOffset-height)+0.5;this._context.save();this._context.translate(x,0);this._context.scale(width/this._maxInnerBarWidth,1);this._context.fillStyle=this._fillStyles[category];this._context.fillRect(0,y,this._maxInnerBarWidth,Math.floor(height));this._context.strokeStyle=WebInspector.TimelineUIUtils.categories()[category].borderColor;this._context.beginPath();this._context.moveTo(0,y);this._context.lineTo(this._maxInnerBarWidth,y);this._context.stroke();this._context.restore();bottomOffset-=height;}
var y0=Math.floor(windowHeight-frame.duration*scale)+0.5;var y1=windowHeight+0.5;this._context.strokeStyle="rgba(90, 90, 90, 0.3)";this._context.beginPath();this._context.moveTo(x,y1);this._context.lineTo(x,y0);this._context.lineTo(x+width,y0);this._context.lineTo(x+width,y1);this._context.stroke();},windowTimes:function(windowLeft,windowRight)
{if(!this._barTimes.length)
return WebInspector.TimelineOverviewBase.prototype.windowTimes.call(this,windowLeft,windowRight);var windowSpan=this._canvas.width;var leftOffset=windowLeft*windowSpan-this._outerPadding+this._actualPadding;var rightOffset=windowRight*windowSpan-this._outerPadding;var firstBar=Math.floor(Math.max(leftOffset,0)/this._actualOuterBarWidth);var lastBar=Math.min(Math.floor(rightOffset/this._actualOuterBarWidth),this._barTimes.length-1);if(firstBar>=this._barTimes.length)
return{startTime:Infinity,endTime:Infinity};const snapToRightTolerancePixels=3;return{startTime:this._barTimes[firstBar].startTime,endTime:(rightOffset+snapToRightTolerancePixels>windowSpan)||(lastBar>=this._barTimes.length)?Infinity:this._barTimes[lastBar].endTime}},windowBoundaries:function(startTime,endTime)
{if(this._barTimes.length===0)
return{left:0,right:1};function barStartComparator(time,barTime)
{return time-barTime.startTime;}
function barEndComparator(time,barTime)
{if(time===barTime.endTime)
return 1;return time-barTime.endTime;}
return{left:this._windowBoundaryFromTime(startTime,barEndComparator),right:this._windowBoundaryFromTime(endTime,barStartComparator)}},_windowBoundaryFromTime:function(time,comparator)
{if(time===Infinity)
return 1;var index=this._firstBarAfter(time,comparator);if(!index)
return 0;return(this._barNumberToScreenPosition(index)-this._actualPadding/2)/this._canvas.width;},_firstBarAfter:function(time,comparator)
{return insertionIndexForObjectInListSortedByFunction(time,this._barTimes,comparator);},__proto__:WebInspector.TimelineOverviewBase.prototype};WebInspector.TimelineMemoryOverview=function(model)
{WebInspector.TimelineOverviewBase.call(this,model);this.element.id="timeline-overview-memory";this._maxHeapSizeLabel=this.element.createChild("div","max memory-graph-label");this._minHeapSizeLabel=this.element.createChild("div","min memory-graph-label");}
WebInspector.TimelineMemoryOverview.prototype={resetHeapSizeLabels:function()
{this._maxHeapSizeLabel.textContent="";this._minHeapSizeLabel.textContent="";},update:function()
{this.resetCanvas();var records=this._model.records();if(!records.length){this.resetHeapSizeLabels();return;}
const lowerOffset=3;var maxUsedHeapSize=0;var minUsedHeapSize=100000000000;var minTime=this._model.minimumRecordTime();var maxTime=this._model.maximumRecordTime();this._model.forAllRecords(function(r){if(!r.counters||!r.counters.jsHeapSizeUsed)
return;maxUsedHeapSize=Math.max(maxUsedHeapSize,r.counters.jsHeapSizeUsed);minUsedHeapSize=Math.min(minUsedHeapSize,r.counters.jsHeapSizeUsed);});minUsedHeapSize=Math.min(minUsedHeapSize,maxUsedHeapSize);var width=this._canvas.width;var height=this._canvas.height-lowerOffset;var xFactor=width/(maxTime-minTime);var yFactor=height/Math.max(maxUsedHeapSize-minUsedHeapSize,1);var histogram=new Array(width);this._model.forAllRecords(function(r){if(!r.counters||!r.counters.jsHeapSizeUsed)
return;var x=Math.round((r.endTime-minTime)*xFactor);var y=(r.counters.jsHeapSizeUsed-minUsedHeapSize)*yFactor;histogram[x]=Math.max(histogram[x]||0,y);});var y=0;var isFirstPoint=true;var ctx=this._context;ctx.save();ctx.translate(0.5,0.5);ctx.beginPath();ctx.moveTo(-1,this._canvas.height);for(var x=0;x<histogram.length;x++){if(typeof histogram[x]==="undefined")
continue;if(isFirstPoint){isFirstPoint=false;y=histogram[x];ctx.lineTo(-1,height-y);}
ctx.lineTo(x,height-y);y=histogram[x];ctx.lineTo(x,height-y);}
ctx.lineTo(width,height-y);ctx.lineTo(width,this._canvas.height);ctx.lineTo(-1,this._canvas.height);ctx.closePath();var gradient=ctx.createLinearGradient(0,0,0,height);gradient.addColorStop(0,"rgba(192,204,255,1)");gradient.addColorStop(1,"rgba(192,204,255,0.4)");ctx.fillStyle=gradient;ctx.fill();ctx.lineWidth=0.5;ctx.strokeStyle="#666";ctx.stroke();ctx.restore();this._maxHeapSizeLabel.textContent=Number.bytesToString(maxUsedHeapSize);this._minHeapSizeLabel.textContent=Number.bytesToString(minUsedHeapSize);},__proto__:WebInspector.TimelineOverviewBase.prototype};WebInspector.TimelinePowerGraph=function(delegate,model)
{WebInspector.CountersGraph.call(this,delegate,model);this._counter=this.createCounter(WebInspector.UIString("Power"),WebInspector.UIString("Power: %.2f\u2009watts"),"#d00");WebInspector.powerProfiler.addEventListener(WebInspector.PowerProfiler.EventTypes.PowerEventRecorded,this._onRecordAdded,this);}
WebInspector.TimelinePowerGraph.prototype={_onRecordAdded:function(event)
{var record=event.data;if(!this._previousRecord){this._previousRecord=record;return;}
this._counter.appendSample(this._previousRecord.timestamp,record.value);this._previousRecord=record;this.scheduleRefresh();},addRecord:function(record)
{},__proto__:WebInspector.CountersGraph.prototype};WebInspector.TimelinePowerOverviewDataProvider=function()
{this._records=[];if(Capabilities.canProfilePower)
WebInspector.powerProfiler.addEventListener(WebInspector.PowerProfiler.EventTypes.PowerEventRecorded,this._onRecordAdded,this);}
WebInspector.TimelinePowerOverviewDataProvider.prototype={records:function()
{return this._records.slice(0,this._records.length-1);},_onRecordAdded:function(event)
{var record=event.data;var length=this._records.length;if(length)
this._records[length-1].value=record.value;this._records.push(record);},__proto__:WebInspector.Object.prototype}
WebInspector.TimelinePowerOverview=function(model)
{WebInspector.TimelineOverviewBase.call(this,model);this.element.id="timeline-overview-power";this._dataProvider=new WebInspector.TimelinePowerOverviewDataProvider();this._maxPowerLabel=this.element.createChild("div","max memory-graph-label");this._minPowerLabel=this.element.createChild("div","min memory-graph-label");}
WebInspector.TimelinePowerOverview.prototype={timelineStarted:function()
{if(Capabilities.canProfilePower)
WebInspector.powerProfiler.startProfile();},timelineStopped:function()
{if(Capabilities.canProfilePower)
WebInspector.powerProfiler.stopProfile();},_resetPowerLabels:function()
{this._maxPowerLabel.textContent="";this._minPowerLabel.textContent="";},update:function()
{this.resetCanvas();var records=this._dataProvider.records();if(!records.length){this._resetPowerLabels();return;}
const lowerOffset=3;var maxPower=0;var minPower=100000000000;var minTime=this._model.minimumRecordTime();var maxTime=this._model.maximumRecordTime();for(var i=0;i<records.length;i++){var record=records[i];if(record.timestamp<minTime||record.timestamp>maxTime)
continue;maxPower=Math.max(maxPower,record.value);minPower=Math.min(minPower,record.value);}
minPower=Math.min(minPower,maxPower);var width=this._canvas.width;var height=this._canvas.height-lowerOffset;var xFactor=width/(maxTime-minTime);var yFactor=height/Math.max(maxPower-minPower,1);var histogram=new Array(width);for(var i=0;i<records.length-1;i++){var record=records[i];if(record.timestamp<minTime||record.timestamp>maxTime)
continue;var x=Math.round((record.timestamp-minTime)*xFactor);var y=Math.round((record.value-minPower)*yFactor);histogram[x]=Math.max(histogram[x]||0,y);}
var y=0;var isFirstPoint=true;var ctx=this._context;ctx.save();ctx.translate(0.5,0.5);ctx.beginPath();ctx.moveTo(-1,this._canvas.height);for(var x=0;x<histogram.length;x++){if(typeof histogram[x]==="undefined")
continue;if(isFirstPoint){isFirstPoint=false;y=histogram[x];ctx.lineTo(-1,height-y);}
ctx.lineTo(x,height-y);y=histogram[x];ctx.lineTo(x,height-y);}
ctx.lineTo(width,height-y);ctx.lineTo(width,this._canvas.height);ctx.lineTo(-1,this._canvas.height);ctx.closePath();ctx.fillStyle="rgba(255,192,0, 0.8);";ctx.fill();ctx.lineWidth=0.5;ctx.strokeStyle="rgba(20,0,0,0.8)";ctx.stroke();ctx.restore();this._maxPowerLabel.textContent=WebInspector.UIString("%.2f\u2009watts",maxPower);this._minPowerLabel.textContent=WebInspector.UIString("%.2f\u2009watts",minPower);;},__proto__:WebInspector.TimelineOverviewBase.prototype};WebInspector.TimelineFlameChartDataProvider=function(model,frameModel)
{WebInspector.FlameChartDataProvider.call(this);this._model=model;this._frameModel=frameModel;this._font="bold 12px "+WebInspector.fontFamily();this._linkifier=new WebInspector.Linkifier();}
WebInspector.TimelineFlameChartDataProvider.prototype={barHeight:function()
{return 20;},textBaseline:function()
{return 6;},textPadding:function()
{return 5;},entryFont:function(entryIndex)
{return this._font;},entryTitle:function(entryIndex)
{var record=this._records[entryIndex];if(record===this._cpuThreadRecord)
return WebInspector.UIString("CPU");else if(record===this._gpuThreadRecord)
return WebInspector.UIString("GPU");var details=WebInspector.TimelineUIUtils.buildDetailsNode(record,this._linkifier);return details?WebInspector.UIString("%s (%s)",record.title(),details.textContent):record.title();},dividerOffsets:function(startTime,endTime)
{if(endTime-startTime<16||endTime-startTime>300)
return null;var frames=this._frameModel.filteredFrames(startTime,endTime);if(frames.length>10)
return null;if(frames.length<3)
return null;var offsets=[];for(var i=0;i<frames.length;++i)
offsets.push(frames[i].startTime);offsets.push(frames.peekLast.endTime)
return offsets;},reset:function()
{this._timelineData=null;},timelineData:function()
{if(this._timelineData)
return this._timelineData;this._linkifier.reset();this._timelineData={entryLevels:[],entryTotalTimes:[],entryOffsets:[]};this._records=[];this._entryThreadDepths={};this._zeroTime=this._model.minimumRecordTime();var cpuThreadRecordPayload={type:WebInspector.TimelineModel.RecordType.Program};this._cpuThreadRecord=new WebInspector.TimelineModel.Record(this._model,(cpuThreadRecordPayload),null);this._pushRecord(this._cpuThreadRecord,0,this.zeroTime(),Math.max(this._model.maximumRecordTime(),this.totalTime()+this.zeroTime()));var gpuThreadRecordPayload={type:WebInspector.TimelineModel.RecordType.Program};this._gpuThreadRecord=new WebInspector.TimelineModel.Record(this._model,(gpuThreadRecordPayload),null);this._pushRecord(this._gpuThreadRecord,0,this.zeroTime(),Math.max(this._model.maximumRecordTime(),this.totalTime()+this.zeroTime()));var records=this._model.records();for(var i=0;i<records.length;++i){var record=records[i];var thread=record.thread;if(thread==="gpu")
continue;if(!thread){for(var j=0;j<record.children.length;++j)
this._appendRecord(record.children[j],1);}else{this._appendRecord(records[i],1);}}
var cpuStackDepth=Math.max(4,this._entryThreadDepths[undefined]);delete this._entryThreadDepths[undefined];var threadBaselines={};var threadBaseline=cpuStackDepth+2;for(var thread in this._entryThreadDepths){threadBaselines[thread]=threadBaseline;threadBaseline+=this._entryThreadDepths[thread];}
this._maxStackDepth=threadBaseline;for(var i=0;i<this._records.length;++i){var record=this._records[i];var level=this._timelineData.entryLevels[i];if(record===this._cpuThreadRecord)
level=0;else if(record===this._gpuThreadRecord)
level=cpuStackDepth+2;else if(record.thread)
level+=threadBaselines[record.thread];this._timelineData.entryLevels[i]=level;}
return this._timelineData;},zeroTime:function()
{return this._zeroTime;},totalTime:function()
{return Math.max(1000,this._model.maximumRecordTime()-this._model.minimumRecordTime());},maxStackDepth:function()
{return this._maxStackDepth;},_appendRecord:function(record,level)
{if(!this._model.isVisible(record)){for(var i=0;i<record.children.length;++i)
this._appendRecord(record.children[i],level);return;}
this._pushRecord(record,level,record.startTime,record.endTime);for(var i=0;i<record.children.length;++i)
this._appendRecord(record.children[i],level+1);},_pushRecord:function(record,level,startTime,endTime)
{var index=this._records.length;this._records.push(record);this._timelineData.entryOffsets[index]=startTime-this._zeroTime;this._timelineData.entryLevels[index]=level;this._timelineData.entryTotalTimes[index]=endTime-startTime;this._entryThreadDepths[record.thread]=Math.max(level,this._entryThreadDepths[record.thread]||0);return index;},prepareHighlightedEntryInfo:function(entryIndex)
{return null;},canJumpToEntry:function(entryIndex)
{return false;},entryColor:function(entryIndex)
{var record=this._records[entryIndex];if(record===this._cpuThreadRecord||record===this._gpuThreadRecord)
return"#555";var category=WebInspector.TimelineUIUtils.categoryForRecord(record);return category.fillColorStop1;},decorateEntry:function(entryIndex,context,text,barX,barY,barWidth,barHeight,offsetToPosition)
{if(barWidth<5)
return false;var record=this._records[entryIndex];var timelineData=this._timelineData;var decorated=false;if(record.children.length){var category=WebInspector.TimelineUIUtils.categoryForRecord(record);if(text){context.fillStyle="white";context.font=this._font;context.fillText(text,barX+this.textPadding(),barY+barHeight-this.textBaseline());}
var entryOffset=timelineData.entryOffsets[entryIndex];var barSelf=offsetToPosition(entryOffset+record.selfTime)
context.beginPath();context.fillStyle=category.backgroundColor;context.rect(barSelf,barY,barX+barWidth-barSelf,barHeight);context.fill();if(text){context.save();context.clip();context.fillStyle=category.borderColor;context.fillText(text,barX+this.textPadding(),barY+barHeight-this.textBaseline());context.restore();}
decorated=true;}
if(record.warnings()||record.childHasWarnings()){context.save();context.rect(barX,barY,barWidth,this.barHeight());context.clip();context.beginPath();context.fillStyle=record.warnings()?"red":"rgba(255, 0, 0, 0.5)";context.moveTo(barX+barWidth-15,barY+1);context.lineTo(barX+barWidth-1,barY+1);context.lineTo(barX+barWidth-1,barY+15);context.fill();context.restore();decorated=true;}
return decorated;},forceDecoration:function(entryIndex)
{var record=this._records[entryIndex];return record.childHasWarnings()||!!record.warnings();},highlightTimeRange:function(entryIndex)
{var record=this._records[entryIndex];if(record===this._cpuThreadRecord||record===this._gpuThreadRecord)
return null;return{startTimeOffset:record.startTime-this._zeroTime,endTimeOffset:record.endTime-this._zeroTime};},paddingLeft:function()
{return 0;},textColor:function(entryIndex)
{return"white";}}
WebInspector.TimelineFlameChart=function(delegate,model,frameModel)
{WebInspector.VBox.call(this);this.element.classList.add("timeline-flamechart");this.registerRequiredCSS("flameChart.css");this._delegate=delegate;this._model=model;this._dataProvider=new WebInspector.TimelineFlameChartDataProvider(model,frameModel);this._mainView=new WebInspector.FlameChart(this._dataProvider,this,true,true);this._mainView.show(this.element);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordingStarted,this._onRecordingStarted,this);this._mainView.addEventListener(WebInspector.FlameChart.Events.EntrySelected,this._onEntrySelected,this);}
WebInspector.TimelineFlameChart.prototype={requestWindowTimes:function(windowStartTime,windowEndTime)
{this._delegate.requestWindowTimes(windowStartTime,windowEndTime);},refreshRecords:function(textFilter)
{this._dataProvider.reset();this._mainView._scheduleUpdate();},wasShown:function()
{this._mainView._scheduleUpdate();},reset:function()
{this._automaticallySizeWindow=true;this._dataProvider.reset();this._mainView.setWindowTimes(0,Infinity);},_onRecordingStarted:function()
{this._automaticallySizeWindow=true;this._mainView.reset();},addRecord:function(record)
{this._dataProvider.reset();if(this._automaticallySizeWindow){var minimumRecordTime=this._model.minimumRecordTime();if(record.startTime>(minimumRecordTime+1000)){this._automaticallySizeWindow=false;this._delegate.requestWindowTimes(minimumRecordTime,minimumRecordTime+1000);}
this._mainView._scheduleUpdate();}else{if(!this._pendingUpdateTimer)
this._pendingUpdateTimer=window.setTimeout(this._updateOnAddRecord.bind(this),300);}},_updateOnAddRecord:function()
{delete this._pendingUpdateTimer;this._mainView._scheduleUpdate();},setWindowTimes:function(startTime,endTime)
{this._mainView.setWindowTimes(startTime,endTime);},setSidebarSize:function(width)
{},highlightSearchResult:function(record,regex,selectRecord)
{},setSelectedRecord:function(record)
{var entryRecords=this._dataProvider._records;for(var entryIndex=0;entryIndex<entryRecords.length;++entryIndex){if(entryRecords[entryIndex]===record){this._mainView.setSelectedEntry(entryIndex);return;}}
this._mainView.setSelectedEntry(-1);},_onEntrySelected:function(event)
{var entryIndex=event.data;var record=this._dataProvider._records[entryIndex];this._delegate.selectRecord(record);},__proto__:WebInspector.VBox.prototype};WebInspector.TimelineUIUtils=function(){}
WebInspector.TimelineUIUtils.categories=function()
{if(WebInspector.TimelineUIUtils._categories)
return WebInspector.TimelineUIUtils._categories;WebInspector.TimelineUIUtils._categories={loading:new WebInspector.TimelineCategory("loading",WebInspector.UIString("Loading"),0,"hsl(214, 53%, 58%)","hsl(214, 67%, 90%)","hsl(214, 67%, 74%)","hsl(214, 67%, 66%)"),scripting:new WebInspector.TimelineCategory("scripting",WebInspector.UIString("Scripting"),1,"hsl(43, 68%, 53%)","hsl(43, 83%, 90%)","hsl(43, 83%, 72%)","hsl(43, 83%, 64%) "),rendering:new WebInspector.TimelineCategory("rendering",WebInspector.UIString("Rendering"),2,"hsl(256, 50%, 60%)","hsl(256, 67%, 90%)","hsl(256, 67%, 76%)","hsl(256, 67%, 70%)"),painting:new WebInspector.TimelineCategory("painting",WebInspector.UIString("Painting"),2,"hsl(109, 33%, 47%)","hsl(109, 33%, 90%)","hsl(109, 33%, 64%)","hsl(109, 33%, 55%)"),other:new WebInspector.TimelineCategory("other",WebInspector.UIString("Other"),-1,"hsl(0, 0%, 73%)","hsl(0, 0%, 90%)","hsl(0, 0%, 87%)","hsl(0, 0%, 79%)"),idle:new WebInspector.TimelineCategory("idle",WebInspector.UIString("Idle"),-1,"hsl(0, 0%, 87%)","hsl(0, 100%, 100%)","hsl(0, 100%, 100%)","hsl(0, 100%, 100%)")};return WebInspector.TimelineUIUtils._categories;};WebInspector.TimelineUIUtils._initRecordStyles=function()
{if(WebInspector.TimelineUIUtils._recordStylesMap)
return WebInspector.TimelineUIUtils._recordStylesMap;var recordTypes=WebInspector.TimelineModel.RecordType;var categories=WebInspector.TimelineUIUtils.categories();var recordStyles={};recordStyles[recordTypes.Root]={title:"#root",category:categories["loading"]};recordStyles[recordTypes.Program]={title:WebInspector.UIString("Other"),category:categories["other"]};recordStyles[recordTypes.EventDispatch]={title:WebInspector.UIString("Event"),category:categories["scripting"]};recordStyles[recordTypes.BeginFrame]={title:WebInspector.UIString("Frame Start"),category:categories["rendering"]};recordStyles[recordTypes.ScheduleStyleRecalculation]={title:WebInspector.UIString("Schedule Style Recalculation"),category:categories["rendering"]};recordStyles[recordTypes.RecalculateStyles]={title:WebInspector.UIString("Recalculate Style"),category:categories["rendering"]};recordStyles[recordTypes.InvalidateLayout]={title:WebInspector.UIString("Invalidate Layout"),category:categories["rendering"]};recordStyles[recordTypes.Layout]={title:WebInspector.UIString("Layout"),category:categories["rendering"]};recordStyles[recordTypes.AutosizeText]={title:WebInspector.UIString("Autosize Text"),category:categories["rendering"]};recordStyles[recordTypes.PaintSetup]={title:WebInspector.UIString("Paint Setup"),category:categories["painting"]};recordStyles[recordTypes.Paint]={title:WebInspector.UIString("Paint"),category:categories["painting"]};recordStyles[recordTypes.Rasterize]={title:WebInspector.UIString("Paint"),category:categories["painting"]};recordStyles[recordTypes.ScrollLayer]={title:WebInspector.UIString("Scroll"),category:categories["rendering"]};recordStyles[recordTypes.DecodeImage]={title:WebInspector.UIString("Image Decode"),category:categories["painting"]};recordStyles[recordTypes.ResizeImage]={title:WebInspector.UIString("Image Resize"),category:categories["painting"]};recordStyles[recordTypes.CompositeLayers]={title:WebInspector.UIString("Composite Layers"),category:categories["painting"]};recordStyles[recordTypes.ParseHTML]={title:WebInspector.UIString("Parse HTML"),category:categories["loading"]};recordStyles[recordTypes.TimerInstall]={title:WebInspector.UIString("Install Timer"),category:categories["scripting"]};recordStyles[recordTypes.TimerRemove]={title:WebInspector.UIString("Remove Timer"),category:categories["scripting"]};recordStyles[recordTypes.TimerFire]={title:WebInspector.UIString("Timer Fired"),category:categories["scripting"]};recordStyles[recordTypes.XHRReadyStateChange]={title:WebInspector.UIString("XHR Ready State Change"),category:categories["scripting"]};recordStyles[recordTypes.XHRLoad]={title:WebInspector.UIString("XHR Load"),category:categories["scripting"]};recordStyles[recordTypes.EvaluateScript]={title:WebInspector.UIString("Evaluate Script"),category:categories["scripting"]};recordStyles[recordTypes.ResourceSendRequest]={title:WebInspector.UIString("Send Request"),category:categories["loading"]};recordStyles[recordTypes.ResourceReceiveResponse]={title:WebInspector.UIString("Receive Response"),category:categories["loading"]};recordStyles[recordTypes.ResourceFinish]={title:WebInspector.UIString("Finish Loading"),category:categories["loading"]};recordStyles[recordTypes.FunctionCall]={title:WebInspector.UIString("Function Call"),category:categories["scripting"]};recordStyles[recordTypes.ResourceReceivedData]={title:WebInspector.UIString("Receive Data"),category:categories["loading"]};recordStyles[recordTypes.GCEvent]={title:WebInspector.UIString("GC Event"),category:categories["scripting"]};recordStyles[recordTypes.MarkDOMContent]={title:WebInspector.UIString("DOMContentLoaded event"),category:categories["scripting"]};recordStyles[recordTypes.MarkLoad]={title:WebInspector.UIString("Load event"),category:categories["scripting"]};recordStyles[recordTypes.MarkFirstPaint]={title:WebInspector.UIString("First paint"),category:categories["painting"]};recordStyles[recordTypes.TimeStamp]={title:WebInspector.UIString("Stamp"),category:categories["scripting"]};recordStyles[recordTypes.ConsoleTime]={title:WebInspector.UIString("Console Time"),category:categories["scripting"]};recordStyles[recordTypes.ScheduleResourceRequest]={title:WebInspector.UIString("Schedule Request"),category:categories["loading"]};recordStyles[recordTypes.RequestAnimationFrame]={title:WebInspector.UIString("Request Animation Frame"),category:categories["scripting"]};recordStyles[recordTypes.CancelAnimationFrame]={title:WebInspector.UIString("Cancel Animation Frame"),category:categories["scripting"]};recordStyles[recordTypes.FireAnimationFrame]={title:WebInspector.UIString("Animation Frame Fired"),category:categories["scripting"]};recordStyles[recordTypes.WebSocketCreate]={title:WebInspector.UIString("Create WebSocket"),category:categories["scripting"]};recordStyles[recordTypes.WebSocketSendHandshakeRequest]={title:WebInspector.UIString("Send WebSocket Handshake"),category:categories["scripting"]};recordStyles[recordTypes.WebSocketReceiveHandshakeResponse]={title:WebInspector.UIString("Receive WebSocket Handshake"),category:categories["scripting"]};recordStyles[recordTypes.WebSocketDestroy]={title:WebInspector.UIString("Destroy WebSocket"),category:categories["scripting"]};recordStyles[recordTypes.EmbedderCallback]={title:WebInspector.UIString("Embedder Callback"),category:categories["scripting"]};WebInspector.TimelineUIUtils._recordStylesMap=recordStyles;return recordStyles;}
WebInspector.TimelineUIUtils.recordStyle=function(record)
{var recordStyles=WebInspector.TimelineUIUtils._initRecordStyles();var result=recordStyles[record.type];if(!result){result={title:WebInspector.UIString("Unknown: %s",record.type),category:WebInspector.TimelineUIUtils.categories()["other"]};recordStyles[record.type]=result;}
return result;}
WebInspector.TimelineUIUtils.categoryForRecord=function(record)
{return WebInspector.TimelineUIUtils.recordStyle(record).category;}
WebInspector.TimelineUIUtils.isEventDivider=function(record)
{var recordTypes=WebInspector.TimelineModel.RecordType;if(record.type===recordTypes.TimeStamp)
return true;if(record.type===recordTypes.MarkFirstPaint)
return true;if(record.type===recordTypes.MarkDOMContent||record.type===recordTypes.MarkLoad){if(record.data&&((typeof record.data.isMainFrame)==="boolean"))
return record.data.isMainFrame;}
return false;}
WebInspector.TimelineUIUtils.needsPreviewElement=function(recordType)
{if(!recordType)
return false;const recordTypes=WebInspector.TimelineModel.RecordType;switch(recordType){case recordTypes.ScheduleResourceRequest:case recordTypes.ResourceSendRequest:case recordTypes.ResourceReceiveResponse:case recordTypes.ResourceReceivedData:case recordTypes.ResourceFinish:return true;default:return false;}}
WebInspector.TimelineUIUtils.createEventDivider=function(recordType,title)
{var eventDivider=document.createElement("div");eventDivider.className="resources-event-divider";var recordTypes=WebInspector.TimelineModel.RecordType;if(recordType===recordTypes.MarkDOMContent)
eventDivider.className+=" resources-blue-divider";else if(recordType===recordTypes.MarkLoad)
eventDivider.className+=" resources-red-divider";else if(recordType===recordTypes.MarkFirstPaint)
eventDivider.className+=" resources-green-divider";else if(recordType===recordTypes.TimeStamp)
eventDivider.className+=" resources-orange-divider";else if(recordType===recordTypes.BeginFrame)
eventDivider.className+=" timeline-frame-divider";if(title)
eventDivider.title=title;return eventDivider;}
WebInspector.TimelineUIUtils.generateMainThreadBarPopupContent=function(model,info)
{var firstTaskIndex=info.firstTaskIndex;var lastTaskIndex=info.lastTaskIndex;var tasks=info.tasks;var messageCount=lastTaskIndex-firstTaskIndex+1;var cpuTime=0;for(var i=firstTaskIndex;i<=lastTaskIndex;++i){var task=tasks[i];cpuTime+=task.endTime-task.startTime;}
var startTime=tasks[firstTaskIndex].startTime;var endTime=tasks[lastTaskIndex].endTime;var duration=endTime-startTime;var contentHelper=new WebInspector.TimelinePopupContentHelper(info.name);var durationText=WebInspector.UIString("%s (at %s)",Number.millisToString(duration,true),Number.millisToString(startTime-model.minimumRecordTime(),true));contentHelper.appendTextRow(WebInspector.UIString("Duration"),durationText);contentHelper.appendTextRow(WebInspector.UIString("CPU time"),Number.millisToString(cpuTime,true));contentHelper.appendTextRow(WebInspector.UIString("Message Count"),messageCount);return contentHelper.contentTable();}
WebInspector.TimelineUIUtils.recordTitle=function(record)
{if(record.type===WebInspector.TimelineModel.RecordType.TimeStamp)
return record.data["message"];if(WebInspector.TimelineUIUtils.isEventDivider(record)){var startTime=Number.millisToString(record.startTimeOffset);return WebInspector.UIString("%s at %s",WebInspector.TimelineUIUtils.recordStyle(record).title,startTime,true);}
return WebInspector.TimelineUIUtils.recordStyle(record).title;}
WebInspector.TimelineUIUtils.aggregateTimeByCategory=function(total,addend)
{for(var category in addend)
total[category]=(total[category]||0)+addend[category];}
WebInspector.TimelineUIUtils.aggregateTimeForRecord=function(total,record)
{var childrenTime=0;var children=record.children;for(var i=0;i<children.length;++i){WebInspector.TimelineUIUtils.aggregateTimeForRecord(total,children[i]);childrenTime+=children[i].endTime-children[i].startTime;}
var categoryName=WebInspector.TimelineUIUtils.recordStyle(record).category.name;var ownTime=record.endTime-record.startTime-childrenTime;total[categoryName]=(total[categoryName]||0)+ownTime;}
WebInspector.TimelineUIUtils._generateAggregatedInfo=function(aggregatedStats)
{var cell=document.createElement("span");cell.className="timeline-aggregated-info";for(var index in aggregatedStats){var label=document.createElement("div");label.className="timeline-aggregated-category timeline-"+index;cell.appendChild(label);var text=document.createElement("span");text.textContent=Number.millisToString(aggregatedStats[index],true);cell.appendChild(text);}
return cell;}
WebInspector.TimelineUIUtils.generatePieChart=function(aggregatedStats,selfCategory,selfTime)
{var element=document.createElement("div");element.className="timeline-aggregated-info";var total=0;for(var categoryName in aggregatedStats)
total+=aggregatedStats[categoryName];function formatter(value)
{return Number.millisToString(value,true);}
var pieChart=new WebInspector.PieChart(total,formatter);element.appendChild(pieChart.element);var footerElement=element.createChild("div","timeline-aggregated-info-legend");if(selfCategory&&selfTime){pieChart.addSlice(selfTime,selfCategory.fillColorStop1);var rowElement=footerElement.createChild("div");rowElement.createChild("div","timeline-aggregated-category timeline-"+selfCategory.name);rowElement.createTextChild(WebInspector.UIString("%s %s (Self)",formatter(selfTime),selfCategory.title));var categoryTime=aggregatedStats[selfCategory.name];var value=categoryTime-selfTime;if(value>0){pieChart.addSlice(value,selfCategory.fillColorStop0);rowElement=footerElement.createChild("div");rowElement.createChild("div","timeline-aggregated-category timeline-"+selfCategory.name);rowElement.createTextChild(WebInspector.UIString("%s %s (Children)",formatter(value),selfCategory.title));}}
for(var categoryName in WebInspector.TimelineUIUtils.categories()){var category=WebInspector.TimelineUIUtils.categories()[categoryName];if(category===selfCategory)
continue;var value=aggregatedStats[category.name];if(!value)
continue;pieChart.addSlice(value,category.fillColorStop0);var rowElement=footerElement.createChild("div");rowElement.createChild("div","timeline-aggregated-category timeline-"+category.name);rowElement.createTextChild(WebInspector.UIString("%s %s",formatter(value),category.title));}
return element;}
WebInspector.TimelineUIUtils.generatePopupContentForFrame=function(frame)
{var contentHelper=new WebInspector.TimelinePopupContentHelper(WebInspector.UIString("Frame"));var durationInMillis=frame.endTime-frame.startTime;var durationText=WebInspector.UIString("%s (at %s)",Number.millisToString(frame.endTime-frame.startTime,true),Number.millisToString(frame.startTimeOffset,true));contentHelper.appendTextRow(WebInspector.UIString("Duration"),durationText);contentHelper.appendTextRow(WebInspector.UIString("FPS"),Math.floor(1000/durationInMillis));contentHelper.appendTextRow(WebInspector.UIString("CPU time"),Number.millisToString(frame.cpuTime,true));contentHelper.appendElementRow(WebInspector.UIString("Aggregated Time"),WebInspector.TimelineUIUtils._generateAggregatedInfo(frame.timeByCategory));if(WebInspector.experimentsSettings.layersPanel.isEnabled()&&frame.layerTree){var layerTreeSnapshot=new WebInspector.LayerTreeSnapshot(frame.layerTree);contentHelper.appendElementRow(WebInspector.UIString("Layer tree"),WebInspector.Linkifier.linkifyUsingRevealer(layerTreeSnapshot,WebInspector.UIString("show")));}
return contentHelper.contentTable();}
WebInspector.TimelineUIUtils.generatePopupContentForFrameStatistics=function(statistics)
{function formatTimeAndFPS(time)
{return WebInspector.UIString("%s (%.0f FPS)",Number.millisToString(time,true),1/time);}
var contentHelper=new WebInspector.TimelineDetailsContentHelper(new WebInspector.Linkifier(),false);contentHelper.appendTextRow(WebInspector.UIString("Minimum Time"),formatTimeAndFPS(statistics.minDuration));contentHelper.appendTextRow(WebInspector.UIString("Average Time"),formatTimeAndFPS(statistics.average));contentHelper.appendTextRow(WebInspector.UIString("Maximum Time"),formatTimeAndFPS(statistics.maxDuration));contentHelper.appendTextRow(WebInspector.UIString("Standard Deviation"),Number.millisToString(statistics.stddev,true));return contentHelper.element;}
WebInspector.TimelineUIUtils.createFillStyle=function(context,width,height,color0,color1,color2)
{var gradient=context.createLinearGradient(0,0,width,height);gradient.addColorStop(0,color0);gradient.addColorStop(0.25,color1);gradient.addColorStop(0.75,color1);gradient.addColorStop(1,color2);return gradient;}
WebInspector.TimelineUIUtils.createFillStyleForCategory=function(context,width,height,category)
{return WebInspector.TimelineUIUtils.createFillStyle(context,width,height,category.fillColorStop0,category.fillColorStop1,category.borderColor);}
WebInspector.TimelineUIUtils.createStyleRuleForCategory=function(category)
{var selector=".timeline-category-"+category.name+" .timeline-graph-bar, "+".panel.timeline .timeline-filters-header .filter-checkbox-filter.filter-checkbox-filter-"+category.name+" .checkbox-filter-checkbox, "+".popover .timeline-"+category.name+", "+".timeline-details-view .timeline-"+category.name+", "+".timeline-category-"+category.name+" .timeline-tree-icon"
return selector+" { background-image: linear-gradient("+
category.fillColorStop0+", "+category.fillColorStop1+" 25%, "+category.fillColorStop1+" 25%, "+category.fillColorStop1+");"+" border-color: "+category.borderColor+"}";}
WebInspector.TimelineUIUtils.generatePopupContent=function(record,linkifier,callback)
{var imageElement=(record.getUserObject("TimelineUIUtils::preview-element")||null);var relatedNode=null;var barrier=new CallbackBarrier();if(!imageElement&&WebInspector.TimelineUIUtils.needsPreviewElement(record.type))
WebInspector.DOMPresentationUtils.buildImagePreviewContents(record.url,false,barrier.createCallback(saveImage));if(record.relatedBackendNodeId())
WebInspector.domModel.pushNodesByBackendIdsToFrontend([record.relatedBackendNodeId()],barrier.createCallback(setRelatedNode));barrier.callWhenDone(callbackWrapper);function saveImage(element)
{imageElement=element||null;record.setUserObject("TimelineUIUtils::preview-element",element);}
function setRelatedNode(nodeIds)
{if(nodeIds)
relatedNode=WebInspector.domModel.nodeForId(nodeIds[0]);}
function callbackWrapper()
{callback(WebInspector.TimelineUIUtils._generatePopupContentSynchronously(record,linkifier,imageElement,relatedNode));}}
WebInspector.TimelineUIUtils._generatePopupContentSynchronously=function(record,linkifier,imagePreviewElement,relatedNode)
{var fragment=document.createDocumentFragment();if(record.children.length)
fragment.appendChild(WebInspector.TimelineUIUtils.generatePieChart(record.aggregatedStats,record.category,record.selfTime));else
fragment.appendChild(WebInspector.TimelineUIUtils.generatePieChart(record.aggregatedStats));const recordTypes=WebInspector.TimelineModel.RecordType;var callSiteStackTraceLabel;var callStackLabel;var relatedNodeLabel;var contentHelper=new WebInspector.TimelineDetailsContentHelper(linkifier,true);contentHelper.appendTextRow(WebInspector.UIString("Self Time"),Number.millisToString(record.selfTime,true));contentHelper.appendTextRow(WebInspector.UIString("Start Time"),Number.millisToString(record.startTimeOffset));switch(record.type){case recordTypes.GCEvent:contentHelper.appendTextRow(WebInspector.UIString("Collected"),Number.bytesToString(record.data["usedHeapSizeDelta"]));break;case recordTypes.TimerFire:callSiteStackTraceLabel=WebInspector.UIString("Timer installed");case recordTypes.TimerInstall:case recordTypes.TimerRemove:contentHelper.appendTextRow(WebInspector.UIString("Timer ID"),record.data["timerId"]);if(typeof record.timeout==="number"){contentHelper.appendTextRow(WebInspector.UIString("Timeout"),Number.millisToString(record.timeout));contentHelper.appendTextRow(WebInspector.UIString("Repeats"),!record.singleShot);}
break;case recordTypes.FireAnimationFrame:callSiteStackTraceLabel=WebInspector.UIString("Animation frame requested");contentHelper.appendTextRow(WebInspector.UIString("Callback ID"),record.data["id"]);break;case recordTypes.FunctionCall:if(record.scriptName)
contentHelper.appendLocationRow(WebInspector.UIString("Location"),record.scriptName,record.scriptLine);break;case recordTypes.ScheduleResourceRequest:case recordTypes.ResourceSendRequest:case recordTypes.ResourceReceiveResponse:case recordTypes.ResourceReceivedData:case recordTypes.ResourceFinish:contentHelper.appendElementRow(WebInspector.UIString("Resource"),WebInspector.linkifyResourceAsNode(record.url));if(imagePreviewElement)
contentHelper.appendElementRow(WebInspector.UIString("Preview"),imagePreviewElement);if(record.data["requestMethod"])
contentHelper.appendTextRow(WebInspector.UIString("Request Method"),record.data["requestMethod"]);if(typeof record.data["statusCode"]==="number")
contentHelper.appendTextRow(WebInspector.UIString("Status Code"),record.data["statusCode"]);if(record.data["mimeType"])
contentHelper.appendTextRow(WebInspector.UIString("MIME Type"),record.data["mimeType"]);if(record.data["encodedDataLength"])
contentHelper.appendTextRow(WebInspector.UIString("Encoded Data Length"),WebInspector.UIString("%d Bytes",record.data["encodedDataLength"]));break;case recordTypes.EvaluateScript:if(record.data&&record.url)
contentHelper.appendLocationRow(WebInspector.UIString("Script"),record.url,record.data["lineNumber"]);break;case recordTypes.Paint:var clip=record.data["clip"];if(clip){contentHelper.appendTextRow(WebInspector.UIString("Location"),WebInspector.UIString("(%d, %d)",clip[0],clip[1]));var clipWidth=WebInspector.TimelineUIUtils._quadWidth(clip);var clipHeight=WebInspector.TimelineUIUtils._quadHeight(clip);contentHelper.appendTextRow(WebInspector.UIString("Dimensions"),WebInspector.UIString("%d × %d",clipWidth,clipHeight));}else{if(typeof record.data["x"]!=="undefined"&&typeof record.data["y"]!=="undefined")
contentHelper.appendTextRow(WebInspector.UIString("Location"),WebInspector.UIString("(%d, %d)",record.data["x"],record.data["y"]));if(typeof record.data["width"]!=="undefined"&&typeof record.data["height"]!=="undefined")
contentHelper.appendTextRow(WebInspector.UIString("Dimensions"),WebInspector.UIString("%d\u2009\u00d7\u2009%d",record.data["width"],record.data["height"]));}
case recordTypes.PaintSetup:case recordTypes.Rasterize:case recordTypes.ScrollLayer:relatedNodeLabel=WebInspector.UIString("Layer root");break;case recordTypes.AutosizeText:relatedNodeLabel=WebInspector.UIString("Root node");break;case recordTypes.DecodeImage:case recordTypes.ResizeImage:relatedNodeLabel=WebInspector.UIString("Image element");if(record.url)
contentHelper.appendElementRow(WebInspector.UIString("Image URL"),WebInspector.linkifyResourceAsNode(record.url));break;case recordTypes.RecalculateStyles:if(record.data["elementCount"])
contentHelper.appendTextRow(WebInspector.UIString("Elements affected"),record.data["elementCount"]);callStackLabel=WebInspector.UIString("Styles recalculation forced");break;case recordTypes.Layout:if(record.data["dirtyObjects"])
contentHelper.appendTextRow(WebInspector.UIString("Nodes that need layout"),record.data["dirtyObjects"]);if(record.data["totalObjects"])
contentHelper.appendTextRow(WebInspector.UIString("Layout tree size"),record.data["totalObjects"]);if(typeof record.data["partialLayout"]==="boolean"){contentHelper.appendTextRow(WebInspector.UIString("Layout scope"),record.data["partialLayout"]?WebInspector.UIString("Partial"):WebInspector.UIString("Whole document"));}
callSiteStackTraceLabel=WebInspector.UIString("Layout invalidated");callStackLabel=WebInspector.UIString("Layout forced");relatedNodeLabel=WebInspector.UIString("Layout root");break;case recordTypes.ConsoleTime:contentHelper.appendTextRow(WebInspector.UIString("Message"),record.data["message"]);break;case recordTypes.WebSocketCreate:case recordTypes.WebSocketSendHandshakeRequest:case recordTypes.WebSocketReceiveHandshakeResponse:case recordTypes.WebSocketDestroy:if(typeof record.webSocketURL!=="undefined")
contentHelper.appendTextRow(WebInspector.UIString("URL"),record.webSocketURL);if(typeof record.webSocketProtocol!=="undefined")
contentHelper.appendTextRow(WebInspector.UIString("WebSocket Protocol"),record.webSocketProtocol);if(typeof record.data["message"]!=="undefined")
contentHelper.appendTextRow(WebInspector.UIString("Message"),record.data["message"]);break;case recordTypes.EmbedderCallback:contentHelper.appendTextRow(WebInspector.UIString("Callback Function"),record.embedderCallbackName);break;default:var detailsNode=WebInspector.TimelineUIUtils.buildDetailsNode(record,linkifier);if(detailsNode)
contentHelper.appendElementRow(WebInspector.UIString("Details"),detailsNode);break;}
if(relatedNode)
contentHelper.appendElementRow(relatedNodeLabel||WebInspector.UIString("Related node"),WebInspector.DOMPresentationUtils.linkifyNodeReference(relatedNode));if(record.scriptName&&record.type!==recordTypes.FunctionCall)
contentHelper.appendLocationRow(WebInspector.UIString("Function Call"),record.scriptName,record.scriptLine);if(record.jsHeapSizeUsed){if(record.usedHeapSizeDelta){var sign=record.usedHeapSizeDelta>0?"+":"-";contentHelper.appendTextRow(WebInspector.UIString("Used JavaScript Heap Size"),WebInspector.UIString("%s (%s%s)",Number.bytesToString(record.jsHeapSizeUsed),sign,Number.bytesToString(Math.abs(record.usedHeapSizeDelta))));}else if(record.category===WebInspector.TimelineUIUtils.categories().scripting)
contentHelper.appendTextRow(WebInspector.UIString("Used JavaScript Heap Size"),Number.bytesToString(record.jsHeapSizeUsed));}
if(record.callSiteStackTrace)
contentHelper.appendStackTrace(callSiteStackTraceLabel||WebInspector.UIString("Call Site stack"),record.callSiteStackTrace);if(record.stackTrace)
contentHelper.appendStackTrace(callStackLabel||WebInspector.UIString("Call Stack"),record.stackTrace);if(record.warnings()){var ul=document.createElement("ul");for(var i=0;i<record.warnings().length;++i)
ul.createChild("li").textContent=record.warnings()[i];contentHelper.appendElementRow(WebInspector.UIString("Warning"),ul);}
fragment.appendChild(contentHelper.element);return fragment;}
WebInspector.TimelineUIUtils._quadWidth=function(quad)
{return Math.round(Math.sqrt(Math.pow(quad[0]-quad[2],2)+Math.pow(quad[1]-quad[3],2)));}
WebInspector.TimelineUIUtils._quadHeight=function(quad)
{return Math.round(Math.sqrt(Math.pow(quad[0]-quad[6],2)+Math.pow(quad[1]-quad[7],2)));}
WebInspector.TimelineUIUtils.buildDetailsNode=function(record,linkifier)
{var details;var detailsText;switch(record.type){case WebInspector.TimelineModel.RecordType.GCEvent:detailsText=WebInspector.UIString("%s collected",Number.bytesToString(record.data["usedHeapSizeDelta"]));break;case WebInspector.TimelineModel.RecordType.TimerFire:detailsText=record.data["timerId"];break;case WebInspector.TimelineModel.RecordType.FunctionCall:if(record.scriptName)
details=linkifyLocation(record.scriptName,record.scriptLine,0);break;case WebInspector.TimelineModel.RecordType.FireAnimationFrame:detailsText=record.data["id"];break;case WebInspector.TimelineModel.RecordType.EventDispatch:detailsText=record.data?record.data["type"]:null;break;case WebInspector.TimelineModel.RecordType.Paint:var width=record.data.clip?WebInspector.TimelineUIUtils._quadWidth(record.data.clip):record.data.width;var height=record.data.clip?WebInspector.TimelineUIUtils._quadHeight(record.data.clip):record.data.height;if(width&&height)
detailsText=WebInspector.UIString("%d\u2009\u00d7\u2009%d",width,height);break;case WebInspector.TimelineModel.RecordType.TimerInstall:case WebInspector.TimelineModel.RecordType.TimerRemove:details=linkifyTopCallFrame();detailsText=record.data["timerId"];break;case WebInspector.TimelineModel.RecordType.RequestAnimationFrame:case WebInspector.TimelineModel.RecordType.CancelAnimationFrame:details=linkifyTopCallFrame();detailsText=record.data["id"];break;case WebInspector.TimelineModel.RecordType.ParseHTML:case WebInspector.TimelineModel.RecordType.RecalculateStyles:details=linkifyTopCallFrame();break;case WebInspector.TimelineModel.RecordType.EvaluateScript:details=record.url?linkifyLocation(record.url,record.data["lineNumber"],0):null;break;case WebInspector.TimelineModel.RecordType.XHRReadyStateChange:case WebInspector.TimelineModel.RecordType.XHRLoad:case WebInspector.TimelineModel.RecordType.ScheduleResourceRequest:case WebInspector.TimelineModel.RecordType.ResourceSendRequest:case WebInspector.TimelineModel.RecordType.ResourceReceivedData:case WebInspector.TimelineModel.RecordType.ResourceReceiveResponse:case WebInspector.TimelineModel.RecordType.ResourceFinish:case WebInspector.TimelineModel.RecordType.DecodeImage:case WebInspector.TimelineModel.RecordType.ResizeImage:detailsText=WebInspector.displayNameForURL(record.url);break;case WebInspector.TimelineModel.RecordType.ConsoleTime:detailsText=record.data["message"];break;case WebInspector.TimelineModel.RecordType.EmbedderCallback:detailsText=record.data["callbackName"];break;default:details=record.scriptName?linkifyLocation(record.scriptName,record.scriptLine,0):linkifyTopCallFrame();break;}
if(!details&&detailsText)
details=document.createTextNode(detailsText);return details;function linkifyLocation(url,lineNumber,columnNumber)
{columnNumber=columnNumber?columnNumber-1:0;return linkifier.linkifyLocation(url,lineNumber-1,columnNumber,"timeline-details");}
function linkifyCallFrame(callFrame)
{return linkifyLocation(callFrame.url,callFrame.lineNumber,callFrame.columnNumber);}
function linkifyTopCallFrame()
{if(record.stackTrace)
return linkifyCallFrame(record.stackTrace[0]);if(record.callSiteStackTrace)
return linkifyCallFrame(record.callSiteStackTrace[0]);return null;}
function linkifyScriptLocation()
{return record.scriptName?linkifyLocation(record.scriptName,record.scriptLine,0):null;}}
WebInspector.TimelineCategory=function(name,title,overviewStripGroupIndex,borderColor,backgroundColor,fillColorStop0,fillColorStop1)
{this.name=name;this.title=title;this.overviewStripGroupIndex=overviewStripGroupIndex;this.borderColor=borderColor;this.backgroundColor=backgroundColor;this.fillColorStop0=fillColorStop0;this.fillColorStop1=fillColorStop1;this.hidden=false;}
WebInspector.TimelineCategory.Events={VisibilityChanged:"VisibilityChanged"};WebInspector.TimelineCategory.prototype={get hidden()
{return this._hidden;},set hidden(hidden)
{this._hidden=hidden;this.dispatchEventToListeners(WebInspector.TimelineCategory.Events.VisibilityChanged,this);},__proto__:WebInspector.Object.prototype}
WebInspector.TimelinePopupContentHelper=function(title)
{this._contentTable=document.createElement("table");var titleCell=this._createCell(WebInspector.UIString("%s - Details",title),"timeline-details-title");titleCell.colSpan=2;var titleRow=document.createElement("tr");titleRow.appendChild(titleCell);this._contentTable.appendChild(titleRow);}
WebInspector.TimelinePopupContentHelper.prototype={contentTable:function()
{return this._contentTable;},_createCell:function(content,styleName)
{var text=document.createElement("label");text.appendChild(document.createTextNode(content));var cell=document.createElement("td");cell.className="timeline-details";if(styleName)
cell.className+=" "+styleName;cell.textContent=content;return cell;},appendTextRow:function(title,content)
{var row=document.createElement("tr");row.appendChild(this._createCell(title,"timeline-details-row-title"));row.appendChild(this._createCell(content,"timeline-details-row-data"));this._contentTable.appendChild(row);},appendElementRow:function(title,content)
{var row=document.createElement("tr");var titleCell=this._createCell(title,"timeline-details-row-title");row.appendChild(titleCell);var cell=document.createElement("td");cell.className="details";if(content instanceof Node)
cell.appendChild(content);else
cell.createTextChild(content||"");row.appendChild(cell);this._contentTable.appendChild(row);}}
WebInspector.TimelineDetailsContentHelper=function(linkifier,monospaceValues)
{this._linkifier=linkifier;this.element=document.createElement("div");this.element.className="timeline-details-view-block";this._monospaceValues=monospaceValues;}
WebInspector.TimelineDetailsContentHelper.prototype={appendTextRow:function(title,value)
{var rowElement=this.element.createChild("div","timeline-details-view-row");rowElement.createChild("span","timeline-details-view-row-title").textContent=WebInspector.UIString("%s: ",title);rowElement.createChild("span","timeline-details-view-row-value"+(this._monospaceValues?" monospace":"")).textContent=value;},appendElementRow:function(title,content)
{var rowElement=this.element.createChild("div","timeline-details-view-row");rowElement.createChild("span","timeline-details-view-row-title").textContent=WebInspector.UIString("%s: ",title);var valueElement=rowElement.createChild("span","timeline-details-view-row-details"+(this._monospaceValues?" monospace":""));if(content instanceof Node)
valueElement.appendChild(content);else
valueElement.createTextChild(content||"");},appendLocationRow:function(title,url,line)
{this.appendElementRow(title,this._linkifier.linkifyLocation(url,line-1)||"");},appendStackTrace:function(title,stackTrace)
{var rowElement=this.element.createChild("div","timeline-details-view-row");rowElement.createChild("span","timeline-details-view-row-title").textContent=WebInspector.UIString("%s: ",title);var stackTraceElement=rowElement.createChild("div","timeline-details-view-row-stack-trace monospace");for(var i=0;i<stackTrace.length;++i){var stackFrame=stackTrace[i];var row=stackTraceElement.createChild("div");row.createTextChild(stackFrame.functionName||WebInspector.UIString("(anonymous function)"));row.createTextChild(" @ ");var urlElement=this._linkifier.linkifyLocation(stackFrame.url,stackFrame.lineNumber-1);row.appendChild(urlElement);}}};WebInspector.TimelineView=function(delegate,model)
{WebInspector.HBox.call(this);this.element.classList.add("timeline-view");this._delegate=delegate;this._model=model;this._presentationModel=new WebInspector.TimelinePresentationModel(model);this._calculator=new WebInspector.TimelineCalculator(model);this._linkifier=new WebInspector.Linkifier();this._boundariesAreValid=true;this._scrollTop=0;this._recordsView=this._createRecordsView();this._recordsView.addEventListener(WebInspector.SplitView.Events.SidebarSizeChanged,this._sidebarResized,this);this._recordsView.show(this.element);this._headerElement=this.element.createChild("div","fill");this._headerElement.id="timeline-graph-records-header";this._cpuBarsElement=this._headerElement.createChild("div","timeline-utilization-strip");if(WebInspector.experimentsSettings.gpuTimeline.isEnabled())
this._gpuBarsElement=this._headerElement.createChild("div","timeline-utilization-strip gpu");this._popoverHelper=new WebInspector.PopoverHelper(this.element,this._getPopoverAnchor.bind(this),this._showPopover.bind(this));this.element.addEventListener("mousemove",this._mouseMove.bind(this),false);this.element.addEventListener("mouseout",this._mouseOut.bind(this),false);this.element.addEventListener("keydown",this._keyDown.bind(this),false);this._expandOffset=15;}
WebInspector.TimelineView.prototype={setFrameModel:function(frameModel)
{this._frameModel=frameModel;},_createRecordsView:function()
{var recordsView=new WebInspector.SplitView(true,false,"timelinePanelRecorsSplitViewState");this._containerElement=recordsView.element;this._containerElement.tabIndex=0;this._containerElement.id="timeline-container";this._containerElement.addEventListener("scroll",this._onScroll.bind(this),false);recordsView.sidebarElement().createChild("div","timeline-records-title").textContent=WebInspector.UIString("RECORDS");this._sidebarListElement=recordsView.sidebarElement().createChild("div","timeline-records-list");this._gridContainer=new WebInspector.VBoxWithResizeCallback(this._onViewportResize.bind(this));this._gridContainer.element.id="resources-container-content";this._gridContainer.show(recordsView.mainElement());this._timelineGrid=new WebInspector.TimelineGrid();this._gridContainer.element.appendChild(this._timelineGrid.element);this._itemsGraphsElement=this._gridContainer.element.createChild("div");this._itemsGraphsElement.id="timeline-graphs";this._topGapElement=this._itemsGraphsElement.createChild("div","timeline-gap");this._graphRowsElement=this._itemsGraphsElement.createChild("div");this._bottomGapElement=this._itemsGraphsElement.createChild("div","timeline-gap");this._expandElements=this._itemsGraphsElement.createChild("div");this._expandElements.id="orphan-expand-elements";return recordsView;},_rootRecord:function()
{return this._presentationModel.rootRecord();},_updateEventDividers:function()
{this._timelineGrid.removeEventDividers();var clientWidth=this._graphRowsElementWidth;var dividers=[];var eventDividerRecords=this._model.eventDividerRecords();for(var i=0;i<eventDividerRecords.length;++i){var record=eventDividerRecords[i];var positions=this._calculator.computeBarGraphWindowPosition(record);var dividerPosition=Math.round(positions.left);if(dividerPosition<0||dividerPosition>=clientWidth||dividers[dividerPosition])
continue;var divider=WebInspector.TimelineUIUtils.createEventDivider(record.type,WebInspector.TimelineUIUtils.recordTitle(record));divider.style.left=dividerPosition+"px";dividers[dividerPosition]=divider;}
this._timelineGrid.addEventDividers(dividers);},_updateFrameBars:function(frames)
{var clientWidth=this._graphRowsElementWidth;if(this._frameContainer)
this._frameContainer.removeChildren();else{const frameContainerBorderWidth=1;this._frameContainer=document.createElement("div");this._frameContainer.classList.add("fill");this._frameContainer.classList.add("timeline-frame-container");this._frameContainer.style.height=WebInspector.TimelinePanel.rowHeight+frameContainerBorderWidth+"px";this._frameContainer.addEventListener("dblclick",this._onFrameDoubleClicked.bind(this),false);}
var dividers=[];for(var i=0;i<frames.length;++i){var frame=frames[i];var frameStart=this._calculator.computePosition(frame.startTime);var frameEnd=this._calculator.computePosition(frame.endTime);var frameStrip=document.createElement("div");frameStrip.className="timeline-frame-strip";var actualStart=Math.max(frameStart,0);var width=frameEnd-actualStart;frameStrip.style.left=actualStart+"px";frameStrip.style.width=width+"px";frameStrip._frame=frame;const minWidthForFrameInfo=60;if(width>minWidthForFrameInfo)
frameStrip.textContent=Number.millisToString(frame.endTime-frame.startTime,true);this._frameContainer.appendChild(frameStrip);if(actualStart>0){var frameMarker=WebInspector.TimelineUIUtils.createEventDivider(WebInspector.TimelineModel.RecordType.BeginFrame);frameMarker.style.left=frameStart+"px";dividers.push(frameMarker);}}
this._timelineGrid.addEventDividers(dividers);this._headerElement.appendChild(this._frameContainer);},_onFrameDoubleClicked:function(event)
{var frameBar=event.target.enclosingNodeOrSelfWithClass("timeline-frame-strip");if(!frameBar)
return;this._delegate.requestWindowTimes(frameBar._frame.startTime,frameBar._frame.endTime);},addRecord:function(record)
{this._presentationModel.addRecord(record);this._invalidateAndScheduleRefresh(false,false);},setSidebarSize:function(width)
{this._recordsView.setSidebarSize(width);},_sidebarResized:function(event)
{this.dispatchEventToListeners(WebInspector.SplitView.Events.SidebarSizeChanged,event.data);},_onViewportResize:function()
{this._resize(this._recordsView.sidebarSize());},_resize:function(sidebarWidth)
{this._closeRecordDetails();this._graphRowsElementWidth=this._graphRowsElement.offsetWidth;this._containerElementHeight=this._containerElement.clientHeight;this._headerElement.style.left=sidebarWidth+"px";this._headerElement.style.width=this._itemsGraphsElement.offsetWidth+"px";this._scheduleRefresh(false,true);},_resetView:function()
{this._windowStartTime=-1;this._windowEndTime=-1;this._boundariesAreValid=false;this._adjustScrollPosition(0);this._linkifier.reset();this._closeRecordDetails();this._automaticallySizeWindow=true;this._presentationModel.reset();},reset:function()
{this._resetView();this._invalidateAndScheduleRefresh(true,true);},elementsToRestoreScrollPositionsFor:function()
{return[this._containerElement];},refreshRecords:function(textFilter)
{this._presentationModel.reset();var records=this._model.records();for(var i=0;i<records.length;++i)
this.addRecord(records[i]);this._automaticallySizeWindow=false;this._presentationModel.setTextFilter(textFilter);this._invalidateAndScheduleRefresh(false,true);},wasShown:function()
{WebInspector.View.prototype.wasShown.call(this);this._onViewportResize();this._refresh();},willHide:function()
{this._closeRecordDetails();WebInspector.View.prototype.willHide.call(this);},_onScroll:function(event)
{this._closeRecordDetails();this._scrollTop=this._containerElement.scrollTop;var dividersTop=Math.max(0,this._scrollTop);this._timelineGrid.setScrollAndDividerTop(this._scrollTop,dividersTop);this._scheduleRefresh(true,true);},_invalidateAndScheduleRefresh:function(preserveBoundaries,userGesture)
{this._presentationModel.invalidateFilteredRecords();this._scheduleRefresh(preserveBoundaries,userGesture);},_selectRecord:function(presentationRecord)
{if(presentationRecord&&presentationRecord.coalesced()){this._innerSetSelectedRecord(presentationRecord);var aggregatedStats={};var presentationChildren=presentationRecord.presentationChildren();for(var i=0;i<presentationChildren.length;++i)
WebInspector.TimelineUIUtils.aggregateTimeByCategory(aggregatedStats,presentationChildren[i].record().aggregatedStats);var idle=presentationRecord.record().endTime-presentationRecord.record().startTime;for(var category in aggregatedStats)
idle-=aggregatedStats[category];aggregatedStats["idle"]=idle;var pieChart=WebInspector.TimelineUIUtils.generatePieChart(aggregatedStats);this._delegate.showInDetails(WebInspector.TimelineUIUtils.recordStyle(presentationRecord.record()).title,pieChart);return;}
this._delegate.selectRecord(presentationRecord?presentationRecord.record():null);},setSelectedRecord:function(record)
{this._innerSetSelectedRecord(this._presentationModel.toPresentationRecord(record));},_innerSetSelectedRecord:function(presentationRecord)
{if(presentationRecord===this._lastSelectedRecord)
return;if(this._lastSelectedRecord){if(this._lastSelectedRecord.listRow())
this._lastSelectedRecord.listRow().renderAsSelected(false);if(this._lastSelectedRecord.graphRow())
this._lastSelectedRecord.graphRow().renderAsSelected(false);}
this._lastSelectedRecord=presentationRecord;if(!presentationRecord)
return;this._innerRevealRecord(presentationRecord);if(presentationRecord.listRow())
presentationRecord.listRow().renderAsSelected(true);if(presentationRecord.graphRow())
presentationRecord.graphRow().renderAsSelected(true);},setWindowTimes:function(startTime,endTime)
{this._windowStartTime=startTime;this._windowEndTime=endTime;this._presentationModel.setWindowTimes(startTime,endTime);this._automaticallySizeWindow=false;this._invalidateAndScheduleRefresh(false,true);this._selectRecord(null);},_scheduleRefresh:function(preserveBoundaries,userGesture)
{this._closeRecordDetails();this._boundariesAreValid&=preserveBoundaries;if(!this.isShowing())
return;if(preserveBoundaries||userGesture)
this._refresh();else{if(!this._refreshTimeout)
this._refreshTimeout=setTimeout(this._refresh.bind(this),300);}},_refresh:function()
{if(this._refreshTimeout){clearTimeout(this._refreshTimeout);delete this._refreshTimeout;}
var windowStartTime=this._windowStartTime;var windowEndTime=this._windowEndTime;this._timelinePaddingLeft=this._expandOffset;if(windowStartTime===-1)
windowStartTime=this._model.minimumRecordTime();if(windowEndTime===-1)
windowEndTime=this._model.maximumRecordTime();this._calculator.setWindow(windowStartTime,windowEndTime);this._calculator.setDisplayWindow(this._timelinePaddingLeft,this._graphRowsElementWidth);this._refreshRecords();if(!this._boundariesAreValid){this._updateEventDividers();if(this._frameContainer)
this._frameContainer.remove();if(this._frameModel){var frames=this._frameModel.filteredFrames(windowStartTime,windowEndTime);const maxFramesForFrameBars=30;if(frames.length&&frames.length<maxFramesForFrameBars){this._timelineGrid.removeDividers();this._updateFrameBars(frames);}else{this._timelineGrid.updateDividers(this._calculator);}}else
this._timelineGrid.updateDividers(this._calculator);this._refreshAllUtilizationBars();}
this._boundariesAreValid=true;},_innerRevealRecord:function(recordToReveal)
{var needRefresh=false;for(var parent=recordToReveal.presentationParent();parent!==this._rootRecord();parent=parent.presentationParent()){if(!parent.collapsed())
continue;this._presentationModel.invalidateFilteredRecords();parent.setCollapsed(false);needRefresh=true;}
var recordsInWindow=this._presentationModel.filteredRecords();var index=recordsInWindow.indexOf(recordToReveal);var itemOffset=index*WebInspector.TimelinePanel.rowHeight;var visibleTop=this._scrollTop-WebInspector.TimelinePanel.headerHeight;var visibleBottom=visibleTop+this._containerElementHeight-WebInspector.TimelinePanel.rowHeight;if(itemOffset<visibleTop)
this._containerElement.scrollTop=itemOffset;else if(itemOffset>visibleBottom)
this._containerElement.scrollTop=itemOffset-this._containerElementHeight+WebInspector.TimelinePanel.headerHeight+WebInspector.TimelinePanel.rowHeight;else if(needRefresh)
this._refreshRecords();},_refreshRecords:function()
{var recordsInWindow=this._presentationModel.filteredRecords();var visibleTop=this._scrollTop;var visibleBottom=visibleTop+this._containerElementHeight;var rowHeight=WebInspector.TimelinePanel.rowHeight;var headerHeight=WebInspector.TimelinePanel.headerHeight;var startIndex=Math.max(0,Math.min(Math.floor((visibleTop-headerHeight)/rowHeight),recordsInWindow.length-1));var endIndex=Math.min(recordsInWindow.length,Math.ceil(visibleBottom/rowHeight));var lastVisibleLine=Math.max(0,Math.floor((visibleBottom-headerHeight)/rowHeight));if(this._automaticallySizeWindow&&recordsInWindow.length>lastVisibleLine){this._automaticallySizeWindow=false;this._selectRecord(null);var windowStartTime=startIndex?recordsInWindow[startIndex].record().startTime:this._model.minimumRecordTime();var windowEndTime=recordsInWindow[Math.max(0,lastVisibleLine-1)].record().endTime;this._delegate.requestWindowTimes(windowStartTime,windowEndTime);recordsInWindow=this._presentationModel.filteredRecords();endIndex=Math.min(recordsInWindow.length,lastVisibleLine);}
this._topGapElement.style.height=(startIndex*rowHeight)+"px";this._recordsView.sidebarElement().firstElementChild.style.flexBasis=(startIndex*rowHeight+headerHeight)+"px";this._bottomGapElement.style.height=(recordsInWindow.length-endIndex)*rowHeight+"px";var rowsHeight=headerHeight+recordsInWindow.length*rowHeight;var totalHeight=Math.max(this._containerElementHeight,rowsHeight);this._recordsView.mainElement().style.height=totalHeight+"px";this._recordsView.sidebarElement().style.height=totalHeight+"px";this._recordsView.resizerElement().style.height=totalHeight+"px";var listRowElement=this._sidebarListElement.firstChild;var width=this._graphRowsElementWidth;this._itemsGraphsElement.removeChild(this._graphRowsElement);var graphRowElement=this._graphRowsElement.firstChild;var scheduleRefreshCallback=this._invalidateAndScheduleRefresh.bind(this,true,true);var selectRecordCallback=this._selectRecord.bind(this);this._itemsGraphsElement.removeChild(this._expandElements);this._expandElements.removeChildren();for(var i=0;i<endIndex;++i){var record=recordsInWindow[i];if(i<startIndex){var lastChildIndex=i+record.visibleChildrenCount();if(lastChildIndex>=startIndex&&lastChildIndex<endIndex){var expandElement=new WebInspector.TimelineExpandableElement(this._expandElements);var positions=this._calculator.computeBarGraphWindowPosition(record);expandElement._update(record,i,positions.left-this._expandOffset,positions.width);}}else{if(!listRowElement){listRowElement=new WebInspector.TimelineRecordListRow(this._linkifier,selectRecordCallback,scheduleRefreshCallback).element;this._sidebarListElement.appendChild(listRowElement);}
if(!graphRowElement){graphRowElement=new WebInspector.TimelineRecordGraphRow(this._itemsGraphsElement,selectRecordCallback,scheduleRefreshCallback).element;this._graphRowsElement.appendChild(graphRowElement);}
listRowElement.row.update(record,visibleTop);graphRowElement.row.update(record,this._calculator,this._expandOffset,i);if(this._lastSelectedRecord===record){listRowElement.row.renderAsSelected(true);graphRowElement.row.renderAsSelected(true);}
listRowElement=listRowElement.nextSibling;graphRowElement=graphRowElement.nextSibling;}}
while(listRowElement){var nextElement=listRowElement.nextSibling;listRowElement.row.dispose();listRowElement=nextElement;}
while(graphRowElement){var nextElement=graphRowElement.nextSibling;graphRowElement.row.dispose();graphRowElement=nextElement;}
this._itemsGraphsElement.insertBefore(this._graphRowsElement,this._bottomGapElement);this._itemsGraphsElement.appendChild(this._expandElements);this._adjustScrollPosition(recordsInWindow.length*rowHeight+headerHeight);return recordsInWindow.length;},_refreshAllUtilizationBars:function()
{this._refreshUtilizationBars(WebInspector.UIString("CPU"),this._model.mainThreadTasks(),this._cpuBarsElement);if(WebInspector.experimentsSettings.gpuTimeline.isEnabled())
this._refreshUtilizationBars(WebInspector.UIString("GPU"),this._model.gpuThreadTasks(),this._gpuBarsElement);},_refreshUtilizationBars:function(name,tasks,container)
{if(!container)
return;const barOffset=3;const minGap=3;var minWidth=WebInspector.TimelineCalculator._minWidth;var widthAdjustment=minWidth/2;var width=this._graphRowsElementWidth;var boundarySpan=this._windowEndTime-this._windowStartTime;var scale=boundarySpan/(width-minWidth-this._timelinePaddingLeft);var startTime=(this._windowStartTime-this._timelinePaddingLeft*scale);var endTime=startTime+width*scale;function compareEndTime(value,task)
{return value<task.endTime?-1:1;}
var taskIndex=insertionIndexForObjectInListSortedByFunction(startTime,tasks,compareEndTime);var foreignStyle="gpu-task-foreign";var element=container.firstChild;var lastElement;var lastLeft;var lastRight;for(;taskIndex<tasks.length;++taskIndex){var task=tasks[taskIndex];if(task.startTime>endTime)
break;var left=Math.max(0,this._calculator.computePosition(task.startTime)+barOffset-widthAdjustment);var right=Math.min(width,this._calculator.computePosition(task.endTime||0)+barOffset+widthAdjustment);if(lastElement){var gap=Math.floor(left)-Math.ceil(lastRight);if(gap<minGap){if(!task.data["foreign"])
lastElement.classList.remove(foreignStyle);lastRight=right;lastElement._tasksInfo.lastTaskIndex=taskIndex;continue;}
lastElement.style.width=(lastRight-lastLeft)+"px";}
if(!element)
element=container.createChild("div","timeline-graph-bar");element.style.left=left+"px";element._tasksInfo={name:name,tasks:tasks,firstTaskIndex:taskIndex,lastTaskIndex:taskIndex};if(task.data["foreign"])
element.classList.add(foreignStyle);lastLeft=left;lastRight=right;lastElement=element;element=element.nextSibling;}
if(lastElement)
lastElement.style.width=(lastRight-lastLeft)+"px";while(element){var nextElement=element.nextSibling;element._tasksInfo=null;container.removeChild(element);element=nextElement;}},_adjustScrollPosition:function(totalHeight)
{if((this._scrollTop+this._containerElementHeight)>totalHeight+1)
this._containerElement.scrollTop=(totalHeight-this._containerElement.offsetHeight);},_getPopoverAnchor:function(element)
{var anchor=element.enclosingNodeOrSelfWithClass("timeline-graph-bar");if(anchor&&anchor._tasksInfo)
return anchor;return element.enclosingNodeOrSelfWithClass("timeline-frame-strip");},_mouseOut:function()
{this._hideQuadHighlight();},_mouseMove:function(e)
{var rowElement=e.target.enclosingNodeOrSelfWithClass("timeline-tree-item");if(rowElement&&rowElement.row&&rowElement.row._record.record().highlightQuad)
this._highlightQuad(rowElement.row._record.record().highlightQuad);else
this._hideQuadHighlight();var taskBarElement=e.target.enclosingNodeOrSelfWithClass("timeline-graph-bar");if(taskBarElement&&taskBarElement._tasksInfo){var offset=taskBarElement.offsetLeft;this._timelineGrid.showCurtains(offset>=0?offset:0,taskBarElement.offsetWidth);}else
this._timelineGrid.hideCurtains();},_keyDown:function(event)
{if(!this._lastSelectedRecord||event.shiftKey||event.metaKey||event.ctrlKey)
return;var record=this._lastSelectedRecord;var recordsInWindow=this._presentationModel.filteredRecords();var index=recordsInWindow.indexOf(record);var recordsInPage=Math.floor(this._containerElementHeight/WebInspector.TimelinePanel.rowHeight);var rowHeight=WebInspector.TimelinePanel.rowHeight;if(index===-1)
index=0;switch(event.keyIdentifier){case"Left":if(record.presentationParent()){if((!record.expandable()||record.collapsed())&&record.presentationParent()!==this._presentationModel.rootRecord()){this._selectRecord(record.presentationParent());}else{record.setCollapsed(true);this._invalidateAndScheduleRefresh(true,true);}}
event.consume(true);break;case"Up":if(--index<0)
break;this._selectRecord(recordsInWindow[index]);event.consume(true);break;case"Right":if(record.expandable()&&record.collapsed()){record.setCollapsed(false);this._invalidateAndScheduleRefresh(true,true);}else{if(++index>=recordsInWindow.length)
break;this._selectRecord(recordsInWindow[index]);}
event.consume(true);break;case"Down":if(++index>=recordsInWindow.length)
break;this._selectRecord(recordsInWindow[index]);event.consume(true);break;case"PageUp":index=Math.max(0,index-recordsInPage);this._scrollTop=Math.max(0,this._scrollTop-recordsInPage*rowHeight);this._containerElement.scrollTop=this._scrollTop;this._selectRecord(recordsInWindow[index]);event.consume(true);break;case"PageDown":index=Math.min(recordsInWindow.length-1,index+recordsInPage);this._scrollTop=Math.min(this._containerElement.scrollHeight-this._containerElementHeight,this._scrollTop+recordsInPage*rowHeight);this._containerElement.scrollTop=this._scrollTop;this._selectRecord(recordsInWindow[index]);event.consume(true);break;case"Home":index=0;this._selectRecord(recordsInWindow[index]);event.consume(true);break;case"End":index=recordsInWindow.length-1;this._selectRecord(recordsInWindow[index]);event.consume(true);break;}},_highlightQuad:function(quad)
{if(this._highlightedQuad===quad)
return;this._highlightedQuad=quad;DOMAgent.highlightQuad(quad,WebInspector.Color.PageHighlight.Content.toProtocolRGBA(),WebInspector.Color.PageHighlight.ContentOutline.toProtocolRGBA());},_hideQuadHighlight:function()
{if(this._highlightedQuad){delete this._highlightedQuad;DOMAgent.hideHighlight();}},_showPopover:function(anchor,popover)
{if(anchor.classList.contains("timeline-frame-strip")){var frame=anchor._frame;popover.show(WebInspector.TimelineUIUtils.generatePopupContentForFrame(frame),anchor);}else if(anchor._tasksInfo){popover.show(WebInspector.TimelineUIUtils.generateMainThreadBarPopupContent(this._model,anchor._tasksInfo),anchor,null,null,WebInspector.Popover.Orientation.Bottom);}
function showCallback(popupContent)
{popover.show(popupContent,anchor);}},_closeRecordDetails:function()
{this._popoverHelper.hidePopover();},highlightSearchResult:function(record,regex,selectRecord)
{if(this._highlightDomChanges)
WebInspector.revertDomChanges(this._highlightDomChanges);this._highlightDomChanges=[];var presentationRecord=this._presentationModel.toPresentationRecord(record);if(!presentationRecord)
return;if(selectRecord)
this._selectRecord(presentationRecord);for(var element=this._sidebarListElement.firstChild;element;element=element.nextSibling){if(element.row._record===presentationRecord){element.row.highlight(regex,this._highlightDomChanges);break;}}},__proto__:WebInspector.HBox.prototype}
WebInspector.TimelineCalculator=function(model)
{this._model=model;}
WebInspector.TimelineCalculator._minWidth=5;WebInspector.TimelineCalculator.prototype={paddingLeft:function()
{return this._paddingLeft;},computePosition:function(time)
{return(time-this._minimumBoundary)/this.boundarySpan()*this._workingArea+this._paddingLeft;},computeBarGraphPercentages:function(record)
{var start=(record.startTime-this._minimumBoundary)/this.boundarySpan()*100;var end=(record.startTime+record.selfTime-this._minimumBoundary)/this.boundarySpan()*100;var endWithChildren=(record.lastChildEndTime-this._minimumBoundary)/this.boundarySpan()*100;var cpuWidth=record.cpuTime/this.boundarySpan()*100;return{start:start,end:end,endWithChildren:endWithChildren,cpuWidth:cpuWidth};},computeBarGraphWindowPosition:function(record)
{var percentages=this.computeBarGraphPercentages(record);var widthAdjustment=0;var left=this.computePosition(record.startTime);var width=(percentages.end-percentages.start)/100*this._workingArea;if(width<WebInspector.TimelineCalculator._minWidth){widthAdjustment=WebInspector.TimelineCalculator._minWidth-width;width=WebInspector.TimelineCalculator._minWidth;}
var widthWithChildren=(percentages.endWithChildren-percentages.start)/100*this._workingArea+widthAdjustment;var cpuWidth=percentages.cpuWidth/100*this._workingArea+widthAdjustment;if(percentages.endWithChildren>percentages.end)
widthWithChildren+=widthAdjustment;return{left:left,width:width,widthWithChildren:widthWithChildren,cpuWidth:cpuWidth};},setWindow:function(minimumBoundary,maximumBoundary)
{this._minimumBoundary=minimumBoundary;this._maximumBoundary=maximumBoundary;},setDisplayWindow:function(paddingLeft,clientWidth)
{this._workingArea=clientWidth-WebInspector.TimelineCalculator._minWidth-paddingLeft;this._paddingLeft=paddingLeft;},formatTime:function(value,precision)
{return Number.preciseMillisToString(value-this.zeroTime(),precision);},maximumBoundary:function()
{return this._maximumBoundary;},minimumBoundary:function()
{return this._minimumBoundary;},zeroTime:function()
{return this._model.minimumRecordTime();},boundarySpan:function()
{return this._maximumBoundary-this._minimumBoundary;}}
WebInspector.TimelineRecordListRow=function(linkifier,selectRecord,scheduleRefresh)
{this.element=document.createElement("div");this.element.row=this;this.element.style.cursor="pointer";this.element.addEventListener("click",this._onClick.bind(this),false);this.element.addEventListener("mouseover",this._onMouseOver.bind(this),false);this.element.addEventListener("mouseout",this._onMouseOut.bind(this),false);this._linkifier=linkifier;this._warningElement=this.element.createChild("div","timeline-tree-item-warning hidden");this._expandArrowElement=this.element.createChild("div","timeline-tree-item-expand-arrow");this._expandArrowElement.addEventListener("click",this._onExpandClick.bind(this),false);var iconElement=this.element.createChild("span","timeline-tree-icon");this._typeElement=this.element.createChild("span","type");this._dataElement=this.element.createChild("span","data dimmed");this._scheduleRefresh=scheduleRefresh;this._selectRecord=selectRecord;}
WebInspector.TimelineRecordListRow.prototype={update:function(presentationRecord,offset)
{this._record=presentationRecord;var record=presentationRecord.record();this._offset=offset;this.element.className="timeline-tree-item timeline-category-"+record.category.name;var paddingLeft=5;var step=-3;for(var currentRecord=presentationRecord.presentationParent()?presentationRecord.presentationParent().presentationParent():null;currentRecord;currentRecord=currentRecord.presentationParent())
paddingLeft+=12/(Math.max(1,step++));this.element.style.paddingLeft=paddingLeft+"px";if(record.thread)
this.element.classList.add("background");this._typeElement.textContent=record.title();if(this._dataElement.firstChild)
this._dataElement.removeChildren();this._warningElement.classList.toggle("hidden",!presentationRecord.hasWarnings()&&!presentationRecord.childHasWarnings());this._warningElement.classList.toggle("timeline-tree-item-child-warning",presentationRecord.childHasWarnings()&&!presentationRecord.hasWarnings());if(presentationRecord.coalesced()){this._dataElement.createTextChild(WebInspector.UIString("× %d",presentationRecord.presentationChildren().length));}else{var detailsNode=WebInspector.TimelineUIUtils.buildDetailsNode(record,this._linkifier);if(detailsNode){this._dataElement.appendChild(document.createTextNode("("));this._dataElement.appendChild(detailsNode);this._dataElement.appendChild(document.createTextNode(")"));}}
this._expandArrowElement.classList.toggle("parent",presentationRecord.expandable());this._expandArrowElement.classList.toggle("expanded",!!presentationRecord.visibleChildrenCount());this._record.setListRow(this);},highlight:function(regExp,domChanges)
{var matchInfo=this.element.textContent.match(regExp);if(matchInfo)
WebInspector.highlightSearchResult(this.element,matchInfo.index,matchInfo[0].length,domChanges);},dispose:function()
{this.element.remove();},_onExpandClick:function(event)
{this._record.setCollapsed(!this._record.collapsed());this._scheduleRefresh();event.consume(true);},_onClick:function(event)
{this._selectRecord(this._record);},renderAsSelected:function(selected)
{this.element.classList.toggle("selected",selected);},_onMouseOver:function(event)
{this.element.classList.add("hovered");if(this._record.graphRow())
this._record.graphRow().element.classList.add("hovered");},_onMouseOut:function(event)
{this.element.classList.remove("hovered");if(this._record.graphRow())
this._record.graphRow().element.classList.remove("hovered");}}
WebInspector.TimelineRecordGraphRow=function(graphContainer,selectRecord,scheduleRefresh)
{this.element=document.createElement("div");this.element.row=this;this.element.addEventListener("mouseover",this._onMouseOver.bind(this),false);this.element.addEventListener("mouseout",this._onMouseOut.bind(this),false);this.element.addEventListener("click",this._onClick.bind(this),false);this._barAreaElement=document.createElement("div");this._barAreaElement.className="timeline-graph-bar-area";this.element.appendChild(this._barAreaElement);this._barWithChildrenElement=document.createElement("div");this._barWithChildrenElement.className="timeline-graph-bar with-children";this._barWithChildrenElement.row=this;this._barAreaElement.appendChild(this._barWithChildrenElement);this._barCpuElement=document.createElement("div");this._barCpuElement.className="timeline-graph-bar cpu"
this._barCpuElement.row=this;this._barAreaElement.appendChild(this._barCpuElement);this._barElement=document.createElement("div");this._barElement.className="timeline-graph-bar";this._barElement.row=this;this._barAreaElement.appendChild(this._barElement);this._expandElement=new WebInspector.TimelineExpandableElement(graphContainer);this._selectRecord=selectRecord;this._scheduleRefresh=scheduleRefresh;}
WebInspector.TimelineRecordGraphRow.prototype={update:function(presentationRecord,calculator,expandOffset,index)
{this._record=presentationRecord;var record=presentationRecord.record();this.element.className="timeline-graph-side timeline-category-"+record.category.name;if(record.thread)
this.element.classList.add("background");var barPosition=calculator.computeBarGraphWindowPosition(record);this._barWithChildrenElement.style.left=barPosition.left+"px";this._barWithChildrenElement.style.width=barPosition.widthWithChildren+"px";this._barElement.style.left=barPosition.left+"px";this._barElement.style.width=(presentationRecord.coalesced()?barPosition.widthWithChildren:barPosition.width)+"px";this._barCpuElement.style.left=barPosition.left+"px";this._barCpuElement.style.width=(presentationRecord.coalesced()?barPosition.widthWithChildren:barPosition.cpuWidth)+"px";this._expandElement._update(presentationRecord,index,barPosition.left-expandOffset,barPosition.width);this._record.setGraphRow(this);},_onClick:function(event)
{if(this._expandElement._arrow.containsEventPoint(event))
this._expand();this._selectRecord(this._record);},renderAsSelected:function(selected)
{this.element.classList.toggle("selected",selected);},_expand:function()
{this._record.setCollapsed(!this._record.collapsed());this._scheduleRefresh();},_onMouseOver:function(event)
{this.element.classList.add("hovered");if(this._record.listRow())
this._record.listRow().element.classList.add("hovered");},_onMouseOut:function(event)
{this.element.classList.remove("hovered");if(this._record.listRow())
this._record.listRow().element.classList.remove("hovered");},dispose:function()
{this.element.remove();this._expandElement._dispose();}}
WebInspector.TimelineExpandableElement=function(container)
{this._element=container.createChild("div","timeline-expandable");this._element.createChild("div","timeline-expandable-left");this._arrow=this._element.createChild("div","timeline-expandable-arrow");}
WebInspector.TimelineExpandableElement.prototype={_update:function(record,index,left,width)
{const rowHeight=WebInspector.TimelinePanel.rowHeight;if(record.visibleChildrenCount()||record.expandable()){this._element.style.top=index*rowHeight+"px";this._element.style.left=left+"px";this._element.style.width=Math.max(12,width+25)+"px";if(!record.collapsed()){this._element.style.height=(record.visibleChildrenCount()+1)*rowHeight+"px";this._element.classList.add("timeline-expandable-expanded");this._element.classList.remove("timeline-expandable-collapsed");}else{this._element.style.height=rowHeight+"px";this._element.classList.add("timeline-expandable-collapsed");this._element.classList.remove("timeline-expandable-expanded");}
this._element.classList.remove("hidden");}else
this._element.classList.add("hidden");},_dispose:function()
{this._element.remove();}};WebInspector.TimelinePanel=function()
{WebInspector.Panel.call(this,"timeline");this.registerRequiredCSS("timelinePanel.css");this.registerRequiredCSS("filter.css");this.element.addEventListener("contextmenu",this._contextMenu.bind(this),false);this._detailsLinkifier=new WebInspector.Linkifier();this._windowStartTime=0;this._windowEndTime=Infinity;this._model=new WebInspector.TimelineModel();this._model.addEventListener(WebInspector.TimelineModel.Events.RecordingStarted,this._onRecordingStarted,this);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordingStopped,this._onRecordingStopped,this);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordsCleared,this._onRecordsCleared,this);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordingProgress,this._onRecordingProgress,this);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordFilterChanged,this._refreshViews,this);this._model.addEventListener(WebInspector.TimelineModel.Events.RecordAdded,this._onRecordAdded,this);this._categoryFilter=new WebInspector.TimelineCategoryFilter();this._durationFilter=new WebInspector.TimelineIsLongFilter();this._textFilter=new WebInspector.TimelineTextFilter();this._model.addFilter(new WebInspector.TimelineHiddenFilter());this._model.addFilter(this._categoryFilter);this._model.addFilter(this._durationFilter);this._model.addFilter(this._textFilter);this._presentationModes=[WebInspector.TimelinePanel.Mode.Events,WebInspector.TimelinePanel.Mode.Frames,WebInspector.TimelinePanel.Mode.Memory];if(WebInspector.experimentsSettings.timelineFlameChart.isEnabled())
this._presentationModes.push(WebInspector.TimelinePanel.Mode.FlameChart);if(Capabilities.canProfilePower)
this._presentationModes.push(WebInspector.TimelinePanel.Mode.Power);this._presentationModeSetting=WebInspector.settings.createSetting("timelineOverviewMode",WebInspector.TimelinePanel.Mode.Events);this._createStatusBarItems();this._topPane=new WebInspector.SplitView(true,false);this._topPane.element.id="timeline-overview-panel";this._topPane.show(this.element);this._topPane.addEventListener(WebInspector.SplitView.Events.SidebarSizeChanged,this._sidebarResized,this);this._topPane.setResizable(false);this._createPresentationSelector();this._overviewPane=new WebInspector.TimelineOverviewPane(this._model);this._overviewPane.addEventListener(WebInspector.TimelineOverviewPane.Events.WindowChanged,this._onWindowChanged.bind(this));this._overviewPane.show(this._topPane.mainElement());this._createFileSelector();this._registerShortcuts();WebInspector.resourceTreeModel.addEventListener(WebInspector.ResourceTreeModel.EventTypes.WillReloadPage,this._willReloadPage,this);WebInspector.resourceTreeModel.addEventListener(WebInspector.ResourceTreeModel.EventTypes.Load,this._loadEventFired,this);this._detailsSplitView=new WebInspector.SplitView(false,true,"timelinePanelDetailsSplitViewState");this._detailsSplitView.element.classList.add("timeline-details-split");this._detailsSplitView.sidebarElement().classList.add("timeline-details");this._detailsView=new WebInspector.TimelineDetailsView();this._detailsSplitView.installResizer(this._detailsView.titleElement());this._detailsView.show(this._detailsSplitView.sidebarElement());this._searchableView=new WebInspector.SearchableView(this);this._searchableView.setMinimumSize(0,25);this._searchableView.element.classList.add("searchable-view");this._searchableView.show(this._detailsSplitView.mainElement());this._stackView=new WebInspector.StackView(false);this._stackView.show(this._searchableView.element);this._stackView.element.classList.add("timeline-view-stack");WebInspector.dockController.addEventListener(WebInspector.DockController.Events.DockSideChanged,this._dockSideChanged.bind(this));WebInspector.settings.splitVerticallyWhenDockedToRight.addChangeListener(this._dockSideChanged.bind(this));this._dockSideChanged();this._selectPresentationMode(this._presentationModeSetting.get());this._detailsSplitView.show(this.element);}
WebInspector.TimelinePanel.Mode={Events:"Events",Frames:"Frames",Memory:"Memory",FlameChart:"FlameChart",Power:"Power"};WebInspector.TimelinePanel.rowHeight=18;WebInspector.TimelinePanel.headerHeight=20;WebInspector.TimelinePanel.durationFilterPresetsMs=[0,1,15];WebInspector.TimelinePanel.prototype={wasShown:function()
{if(!WebInspector.TimelinePanel._categoryStylesInitialized){WebInspector.TimelinePanel._categoryStylesInitialized=true;var style=document.createElement("style");var categories=WebInspector.TimelineUIUtils.categories();style.textContent=Object.values(categories).map(WebInspector.TimelineUIUtils.createStyleRuleForCategory).join("\n");document.head.appendChild(style);}},_dockSideChanged:function()
{var dockSide=WebInspector.dockController.dockSide();var vertically=false;if(dockSide===WebInspector.DockController.State.DockedToBottom)
vertically=true;else
vertically=!WebInspector.settings.splitVerticallyWhenDockedToRight.get();this._detailsSplitView.setVertical(vertically);this._detailsView.setVertical(vertically);},windowStartTime:function()
{if(this._windowStartTime)
return this._windowStartTime;if(this._model.minimumRecordTime()!=-1)
return this._model.minimumRecordTime();return 0;},windowEndTime:function()
{if(this._windowEndTime<Infinity)
return this._windowEndTime;if(this._model.maximumRecordTime()!=-1)
return this._model.maximumRecordTime();return Infinity;},_sidebarResized:function(event)
{var width=(event.data);this._topPane.setSidebarSize(width);for(var i=0;i<this._currentViews.length;++i)
this._currentViews[i].setSidebarSize(width);},_onWindowChanged:function(event)
{this._windowStartTime=event.data.startTime;this._windowEndTime=event.data.endTime;for(var i=0;i<this._currentViews.length;++i)
this._currentViews[i].setWindowTimes(this._windowStartTime,this._windowEndTime);this._updateSelectionDetails();},requestWindowTimes:function(windowStartTime,windowEndTime)
{this._overviewPane.requestWindowTimes(windowStartTime,windowEndTime);},_frameModel:function()
{if(!this._lazyFrameModel)
this._lazyFrameModel=new WebInspector.TimelineFrameModel(this._model);return this._lazyFrameModel;},_timelineView:function()
{if(!this._lazyTimelineView)
this._lazyTimelineView=new WebInspector.TimelineView(this,this._model);return this._lazyTimelineView;},_viewsForMode:function(mode)
{var views=this._viewsMap[mode];if(!views){views={};switch(mode){case WebInspector.TimelinePanel.Mode.Events:views.overviewView=new WebInspector.TimelineEventOverview(this._model);views.mainViews=[this._timelineView()];break;case WebInspector.TimelinePanel.Mode.Frames:views.overviewView=new WebInspector.TimelineFrameOverview(this._model,this._frameModel());views.mainViews=[this._timelineView()];break;case WebInspector.TimelinePanel.Mode.Memory:views.overviewView=new WebInspector.TimelineMemoryOverview(this._model);views.mainViews=[this._timelineView(),new WebInspector.MemoryCountersGraph(this,this._model)];break;case WebInspector.TimelinePanel.Mode.FlameChart:views.overviewView=new WebInspector.TimelineFrameOverview(this._model,this._frameModel());views.mainViews=[new WebInspector.TimelineFlameChart(this,this._model,this._frameModel())];break;case WebInspector.TimelinePanel.Mode.Power:views.overviewView=new WebInspector.TimelinePowerOverview(this._model);views.mainViews=[this._timelineView(),new WebInspector.TimelinePowerGraph(this,this._model)];break;default:console.assert(false,"Unknown mode: "+mode);}
for(var i=0;i<views.mainViews.length;++i)
views.mainViews[i].addEventListener(WebInspector.SplitView.Events.SidebarSizeChanged,this._sidebarResized,this);this._viewsMap[mode]=views;}
this._timelineView().setFrameModel(mode===WebInspector.TimelinePanel.Mode.Frames?this._frameModel():null);return views;},_createPresentationSelector:function()
{this._viewsMap={};var topPaneSidebarElement=this._topPane.sidebarElement();topPaneSidebarElement.id="timeline-overview-sidebar";var overviewTreeElement=topPaneSidebarElement.createChild("ol","sidebar-tree vbox");var topPaneSidebarTree=new TreeOutline(overviewTreeElement);this._overviewItems={};for(var i=0;i<this._presentationModes.length;++i){var mode=this._presentationModes[i];this._overviewItems[mode]=new WebInspector.SidebarTreeElement("timeline-overview-sidebar-"+mode.toLowerCase(),WebInspector.UIString(mode));var item=this._overviewItems[mode];item.onselect=this._onModeChanged.bind(this,mode);topPaneSidebarTree.appendChild(item);}},_createStatusBarItems:function()
{var panelStatusBarElement=this.element.createChild("div","panel-status-bar");this._statusBarButtons=([]);this.toggleTimelineButton=new WebInspector.StatusBarButton(WebInspector.UIString("Record"),"record-profile-status-bar-item");this.toggleTimelineButton.addEventListener("click",this._toggleTimelineButtonClicked,this);this._statusBarButtons.push(this.toggleTimelineButton);panelStatusBarElement.appendChild(this.toggleTimelineButton.element);this.clearButton=new WebInspector.StatusBarButton(WebInspector.UIString("Clear"),"clear-status-bar-item");this.clearButton.addEventListener("click",this._onClearButtonClick,this);this._statusBarButtons.push(this.clearButton);panelStatusBarElement.appendChild(this.clearButton.element);this._filterBar=this._createFilterBar();panelStatusBarElement.appendChild(this._filterBar.filterButton().element);this.garbageCollectButton=new WebInspector.StatusBarButton(WebInspector.UIString("Collect Garbage"),"garbage-collect-status-bar-item");this.garbageCollectButton.addEventListener("click",this._garbageCollectButtonClicked,this);this._statusBarButtons.push(this.garbageCollectButton);panelStatusBarElement.appendChild(this.garbageCollectButton.element);panelStatusBarElement.appendChild(WebInspector.SettingsUI.createSettingCheckbox(WebInspector.UIString("Capture stacks"),WebInspector.settings.timelineCaptureStacks,true,undefined,WebInspector.UIString("Capture JavaScript stack on every timeline event")));this._miscStatusBarItems=panelStatusBarElement.createChild("div","status-bar-item");this._filtersContainer=this.element.createChild("div","timeline-filters-header hidden");this._filtersContainer.appendChild(this._filterBar.filtersElement());this._filterBar.addEventListener(WebInspector.FilterBar.Events.FiltersToggled,this._onFiltersToggled,this);this._filterBar.setName("timelinePanel");},_createFilterBar:function()
{this._filterBar=new WebInspector.FilterBar();this._filters={};this._filters._textFilterUI=new WebInspector.TextFilterUI();this._filters._textFilterUI.addEventListener(WebInspector.FilterUI.Events.FilterChanged,this._textFilterChanged,this);this._filterBar.addFilter(this._filters._textFilterUI);var durationOptions=[];for(var presetIndex=0;presetIndex<WebInspector.TimelinePanel.durationFilterPresetsMs.length;++presetIndex){var durationMs=WebInspector.TimelinePanel.durationFilterPresetsMs[presetIndex];var durationOption={};if(!durationMs){durationOption.label=WebInspector.UIString("All");durationOption.title=WebInspector.UIString("Show all records");}else{durationOption.label=WebInspector.UIString("\u2265 %dms",durationMs);durationOption.title=WebInspector.UIString("Hide records shorter than %dms",durationMs);}
durationOption.value=durationMs;durationOptions.push(durationOption);}
this._filters._durationFilterUI=new WebInspector.ComboBoxFilterUI(durationOptions);this._filters._durationFilterUI.addEventListener(WebInspector.FilterUI.Events.FilterChanged,this._durationFilterChanged,this);this._filterBar.addFilter(this._filters._durationFilterUI);this._filters._categoryFiltersUI={};var categoryTypes=[];var categories=WebInspector.TimelineUIUtils.categories();for(var categoryName in categories){var category=categories[categoryName];if(category.overviewStripGroupIndex<0)
continue;var filter=new WebInspector.CheckboxFilterUI(category.name,category.title);this._filters._categoryFiltersUI[category.name]=filter;filter.addEventListener(WebInspector.FilterUI.Events.FilterChanged,this._categoriesFilterChanged.bind(this,categoryName),this);this._filterBar.addFilter(filter);}
return this._filterBar;},_textFilterChanged:function(event)
{var searchQuery=this._filters._textFilterUI.value();this.searchCanceled();this._textFilter.setRegex(searchQuery?createPlainTextSearchRegex(searchQuery,"i"):null);},_durationFilterChanged:function()
{var duration=this._filters._durationFilterUI.value();var minimumRecordDuration=parseInt(duration,10);this._durationFilter.setMinimumRecordDuration(minimumRecordDuration);},_categoriesFilterChanged:function(name,event)
{var categories=WebInspector.TimelineUIUtils.categories();categories[name].hidden=!this._filters._categoryFiltersUI[name].checked();this._categoryFilter.notifyFilterChanged();},defaultFocusedElement:function()
{return this.element;},_onFiltersToggled:function(event)
{var toggled=(event.data);this._filtersContainer.classList.toggle("hidden",!toggled);this.doResize();},_prepareToLoadTimeline:function()
{if(this._operationInProgress)
return null;if(this._recordingInProgress()){this.toggleTimelineButton.toggled=false;this._stopRecording();}
var progressIndicator=new WebInspector.ProgressIndicator();progressIndicator.addEventListener(WebInspector.ProgressIndicator.Events.Done,this._setOperationInProgress.bind(this,null));this._setOperationInProgress(progressIndicator);return progressIndicator;},_setOperationInProgress:function(indicator)
{this._operationInProgress=!!indicator;for(var i=0;i<this._statusBarButtons.length;++i)
this._statusBarButtons[i].setEnabled(!this._operationInProgress);this._miscStatusBarItems.removeChildren();if(indicator)
this._miscStatusBarItems.appendChild(indicator.element);},_registerShortcuts:function()
{this.registerShortcuts(WebInspector.ShortcutsScreen.TimelinePanelShortcuts.StartStopRecording,this._toggleTimelineButtonClicked.bind(this));this.registerShortcuts(WebInspector.ShortcutsScreen.TimelinePanelShortcuts.SaveToFile,this._saveToFile.bind(this));this.registerShortcuts(WebInspector.ShortcutsScreen.TimelinePanelShortcuts.LoadFromFile,this._selectFileToLoad.bind(this));},_createFileSelector:function()
{if(this._fileSelectorElement)
this._fileSelectorElement.remove();this._fileSelectorElement=WebInspector.createFileSelectorElement(this._loadFromFile.bind(this));this.element.appendChild(this._fileSelectorElement);},_contextMenu:function(event)
{var contextMenu=new WebInspector.ContextMenu(event);contextMenu.appendItem(WebInspector.UIString(WebInspector.useLowerCaseMenuTitles()?"Save Timeline data\u2026":"Save Timeline Data\u2026"),this._saveToFile.bind(this),this._operationInProgress);contextMenu.appendItem(WebInspector.UIString(WebInspector.useLowerCaseMenuTitles()?"Load Timeline data\u2026":"Load Timeline Data\u2026"),this._selectFileToLoad.bind(this),this._operationInProgress);contextMenu.show();},_saveToFile:function()
{if(this._operationInProgress)
return true;this._model.saveToFile();return true;},_selectFileToLoad:function(){this._fileSelectorElement.click();return true;},_loadFromFile:function(file)
{var progressIndicator=this._prepareToLoadTimeline();if(!progressIndicator)
return;this._model.loadFromFile(file,progressIndicator);this._createFileSelector();},loadFromURL:function(url)
{var progressIndicator=this._prepareToLoadTimeline();if(!progressIndicator)
return;this._model.loadFromURL(url,progressIndicator);},_selectPresentationMode:function(mode)
{if(!this._overviewItems[mode])
mode=WebInspector.TimelinePanel.Mode.Events;this._overviewItems[mode].revealAndSelect(false);},_refreshViews:function(totalUpdate)
{for(var i=0;i<this._currentViews.length;++i){var view=this._currentViews[i];view.refreshRecords(this._textFilter._regex);}
this._updateSelectionDetails();},_onModeChanged:function(mode)
{this.element.classList.remove("timeline-"+this._presentationModeSetting.get().toLowerCase()+"-view");this._presentationModeSetting.set(mode);this.element.classList.add("timeline-"+mode.toLowerCase()+"-view");this._stackView.detachChildViews();var views=this._viewsForMode(mode);this._currentViews=views.mainViews;for(var i=0;i<this._currentViews.length;++i){var view=this._currentViews[i];view.setWindowTimes(this.windowStartTime(),this.windowEndTime());this._stackView.appendView(view,"timelinePanelTimelineStackSplitViewState");view.refreshRecords(this._textFilter._regex);}
this._overviewControl=views.overviewView;this._overviewPane.setOverviewControl(this._overviewControl);this._updateSelectionDetails();},_startRecording:function(userInitiated)
{this._userInitiatedRecording=userInitiated;this._model.startRecording();for(var i=0;i<this._presentationModes.length;++i)
this._viewsForMode(this._presentationModes[i]).overviewView.timelineStarted();if(userInitiated)
WebInspector.userMetrics.TimelineStarted.record();},_stopRecording:function()
{this._userInitiatedRecording=false;this._model.stopRecording();for(var i=0;i<this._presentationModes.length;++i)
this._viewsForMode(this._presentationModes[i]).overviewView.timelineStopped();},_toggleTimelineButtonClicked:function()
{if(this._operationInProgress)
return true;if(this._recordingInProgress())
this._stopRecording();else
this._startRecording(true);return true;},_garbageCollectButtonClicked:function()
{HeapProfilerAgent.collectGarbage();},_onClearButtonClick:function()
{this._model.reset();},_onRecordsCleared:function()
{this.requestWindowTimes(0,Infinity);delete this._selectedRecord;if(this._lazyFrameModel)
this._lazyFrameModel.reset();for(var i=0;i<this._currentViews.length;++i)
this._currentViews[i].reset();this._overviewControl.reset();this._updateSelectionDetails();},_onRecordingStarted:function()
{this.toggleTimelineButton.title=WebInspector.UIString("Stop");this.toggleTimelineButton.toggled=true;this._showProgressPane();},_recordingInProgress:function()
{return this.toggleTimelineButton.toggled;},_showProgressPane:function()
{if(!WebInspector.experimentsSettings.timelineNoLiveUpdate.isEnabled())
return;this._hideProgressPane();this._progressElement=this._detailsSplitView.mainElement().createChild("div","timeline-progress-pane");this._progressElement.textContent=WebInspector.UIString("%d events collected",0);},_hideProgressPane:function()
{if(!WebInspector.experimentsSettings.timelineNoLiveUpdate.isEnabled())
return;if(this._progressElement)
this._progressElement.remove();},_onRecordingProgress:function(event)
{if(!WebInspector.experimentsSettings.timelineNoLiveUpdate.isEnabled())
return;this._progressElement.textContent=WebInspector.UIString("%d events collected",event.data);},_onRecordingStopped:function()
{this.toggleTimelineButton.title=WebInspector.UIString("Record");this.toggleTimelineButton.toggled=false;this._hideProgressPane();},_onRecordAdded:function(event)
{this._addRecord((event.data));},_addRecord:function(record)
{if(this._lazyFrameModel)
this._lazyFrameModel.addRecord(record);for(var i=0;i<this._currentViews.length;++i)
this._currentViews[i].addRecord(record);this._overviewPane.addRecord(record);this._updateSearchHighlight(false,true);},_willReloadPage:function(event)
{if(this._operationInProgress||this._userInitiatedRecording||!this.isShowing())
return;this._startRecording(false);},_loadEventFired:function(event)
{if(!this._recordingInProgress()||this._userInitiatedRecording)
return;this._stopRecording();},jumpToNextSearchResult:function()
{if(!this._searchResults||!this._searchResults.length)
return;var index=this._selectedSearchResult?this._searchResults.indexOf(this._selectedSearchResult):-1;this._jumpToSearchResult(index+1);},jumpToPreviousSearchResult:function()
{if(!this._searchResults||!this._searchResults.length)
return;var index=this._selectedSearchResult?this._searchResults.indexOf(this._selectedSearchResult):0;this._jumpToSearchResult(index-1);},_jumpToSearchResult:function(index)
{this._selectSearchResult((index+this._searchResults.length)%this._searchResults.length);this._currentViews[0].highlightSearchResult(this._selectedSearchResult,this._searchRegex,true);},_selectSearchResult:function(index)
{this._selectedSearchResult=this._searchResults[index];this._searchableView.updateCurrentMatchIndex(index);},_clearHighlight:function()
{this._currentViews[0].highlightSearchResult(null);},_updateSearchHighlight:function(revealRecord,shouldJump)
{if(this._textFilter||!this._searchRegex){this._clearHighlight();return;}
if(!this._searchResults)
this._updateSearchResults(shouldJump);this._currentViews[0].highlightSearchResult(this._selectedSearchResult,this._searchRegex,revealRecord);},_updateSearchResults:function(shouldJump)
{var searchRegExp=this._searchRegex;if(!searchRegExp)
return;var matches=[];function processRecord(record)
{if(record.testContentMatching(searchRegExp))
matches.push(record);}
this._model.forAllFilteredRecords(processRecord);var matchesCount=matches.length;if(matchesCount){this._searchResults=matches;this._searchableView.updateSearchMatchesCount(matchesCount);var selectedIndex=matches.indexOf(this._selectedSearchResult);if(shouldJump&&selectedIndex===-1)
selectedIndex=0;this._selectSearchResult(selectedIndex);}else{this._searchableView.updateSearchMatchesCount(0);delete this._selectedSearchResult;}},searchCanceled:function()
{this._clearHighlight();delete this._searchResults;delete this._selectedSearchResult;delete this._searchRegex;},performSearch:function(query,shouldJump)
{this._searchRegex=createPlainTextSearchRegex(query,"i");delete this._searchResults;this._updateSearchHighlight(true,shouldJump);},_updateSelectionDetails:function()
{if(this._selectedRecord)
return;var startTime=this._windowStartTime;var endTime=this._windowEndTime;if(startTime<0)
return;var aggregatedStats={};function compareEndTime(value,task)
{return value<task.endTime?-1:1;}
function aggregateTimeForRecordWithinWindow(record)
{if(!record.endTime||record.endTime<startTime||record.startTime>endTime)
return;var childrenTime=0;var children=record.children||[];for(var i=0;i<children.length;++i){var child=children[i];if(!child.endTime||child.endTime<startTime||child.startTime>endTime)
continue;childrenTime+=Math.min(endTime,child.endTime)-Math.max(startTime,child.startTime);aggregateTimeForRecordWithinWindow(child);}
var categoryName=WebInspector.TimelineUIUtils.categoryForRecord(record).name;var ownTime=Math.min(endTime,record.endTime)-Math.max(startTime,record.startTime)-childrenTime;aggregatedStats[categoryName]=(aggregatedStats[categoryName]||0)+ownTime;}
var mainThreadTasks=this._model.mainThreadTasks();var taskIndex=insertionIndexForObjectInListSortedByFunction(startTime,mainThreadTasks,compareEndTime);for(;taskIndex<mainThreadTasks.length;++taskIndex){var task=mainThreadTasks[taskIndex];if(task.startTime>endTime)
break;aggregateTimeForRecordWithinWindow(task);}
var aggregatedTotal=0;for(var categoryName in aggregatedStats)
aggregatedTotal+=aggregatedStats[categoryName];aggregatedStats["idle"]=Math.max(0,endTime-startTime-aggregatedTotal);var fragment=document.createDocumentFragment();fragment.appendChild(WebInspector.TimelineUIUtils.generatePieChart(aggregatedStats));var startOffset=startTime-this._model.minimumRecordTime();var endOffset=endTime-this._model.minimumRecordTime();var title=WebInspector.UIString("%s \u2013 %s",Number.millisToString(startOffset),Number.millisToString(endOffset));this._detailsView.setContent(title,fragment);},selectRecord:function(record)
{this._detailsLinkifier.reset();this._selectedRecord=record;if(!record){this._updateSelectionDetails();return;}
for(var i=0;i<this._currentViews.length;++i){var view=this._currentViews[i];view.setSelectedRecord(record);}
if(!record){this._updateSelectionDetails();return;}
WebInspector.TimelineUIUtils.generatePopupContent(record,this._detailsLinkifier,showCallback.bind(this));function showCallback(element)
{this._detailsView.setContent(record.title(),element);}},showInDetails:function(title,node)
{this._detailsView.setContent(title,node);},__proto__:WebInspector.Panel.prototype}
WebInspector.TimelineDetailsView=function()
{WebInspector.VBox.call(this);this.element.classList.add("timeline-details-view");this._titleElement=this.element.createChild("div","timeline-details-view-title");this._titleElement.textContent=WebInspector.UIString("DETAILS");this._contentElement=this.element.createChild("div","timeline-details-view-body");}
WebInspector.TimelineDetailsView.prototype={titleElement:function()
{return this._titleElement;},setContent:function(title,node)
{this._titleElement.textContent=WebInspector.UIString("DETAILS: %s",title);this._contentElement.removeChildren();this._contentElement.appendChild(node);},setVertical:function(vertical)
{this._contentElement.classList.toggle("hbox",!vertical);this._contentElement.classList.toggle("vbox",vertical);},__proto__:WebInspector.VBox.prototype}
WebInspector.TimelineModeView=function()
{}
WebInspector.TimelineModeView.prototype={reset:function(){},refreshRecords:function(textFilter){},addRecord:function(record){},highlightSearchResult:function(record,regex,selectRecord){},setWindowTimes:function(startTime,endTime){},setSidebarSize:function(width){},setSelectedRecord:function(record){}}
WebInspector.TimelineModeViewDelegate=function(){}
WebInspector.TimelineModeViewDelegate.prototype={requestWindowTimes:function(startTime,endTime){},selectRecord:function(record){},showInDetails:function(title,node){},}
WebInspector.TimelineCategoryFilter=function()
{WebInspector.TimelineModel.Filter.call(this);}
WebInspector.TimelineCategoryFilter.prototype={accept:function(record)
{return!record.category.hidden;},__proto__:WebInspector.TimelineModel.Filter.prototype}
WebInspector.TimelineIsLongFilter=function()
{WebInspector.TimelineModel.Filter.call(this);this._minimumRecordDuration=0;}
WebInspector.TimelineIsLongFilter.prototype={setMinimumRecordDuration:function(value)
{this._minimumRecordDuration=value;this.notifyFilterChanged();},accept:function(record)
{return this._minimumRecordDuration?((record.lastChildEndTime-record.startTime)>=this._minimumRecordDuration):true;},__proto__:WebInspector.TimelineModel.Filter.prototype}
WebInspector.TimelineTextFilter=function()
{WebInspector.TimelineModel.Filter.call(this);}
WebInspector.TimelineTextFilter.prototype={setRegex:function(regex)
{this._regex=regex;this.notifyFilterChanged();},accept:function(record)
{if(!this._regex)
return true;var accept=false;function processRecord(record)
{return record.testContentMatching(this._regex);}
return WebInspector.TimelineModel.forAllRecords([record],processRecord.bind(this));},__proto__:WebInspector.TimelineModel.Filter.prototype}
WebInspector.TimelineHiddenFilter=function()
{WebInspector.TimelineModel.Filter.call(this);this._hiddenRecords={};this._hiddenRecords[WebInspector.TimelineModel.RecordType.MarkDOMContent]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.MarkLoad]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.MarkFirstPaint]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.GPUTask]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.ScheduleStyleRecalculation]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.InvalidateLayout]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.RequestMainThreadFrame]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.ActivateLayerTree]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.DrawFrame]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.BeginFrame]=1;this._hiddenRecords[WebInspector.TimelineModel.RecordType.UpdateLayerTree]=1;}
WebInspector.TimelineHiddenFilter.prototype={accept:function(record)
{return!this._hiddenRecords[record.type];},__proto__:WebInspector.TimelineModel.Filter.prototype}