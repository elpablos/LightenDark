WebInspector.LayerTree=function(model,treeOutline)
{WebInspector.Object.call(this);this._model=model;this._treeOutline=treeOutline;this._treeOutline.childrenListElement.addEventListener("mousemove",this._onMouseMove.bind(this),false);this._treeOutline.childrenListElement.addEventListener("mouseout",this._onMouseMove.bind(this),false);this._treeOutline.childrenListElement.addEventListener("contextmenu",this._onContextMenu.bind(this),true);this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerTreeChanged,this._update.bind(this));this._lastHoveredNode=null;}
WebInspector.LayerTree.Events={LayerHovered:"LayerHovered",LayerSelected:"LayerSelected"}
WebInspector.LayerTree.prototype={selectLayer:function(layer)
{this.hoverLayer(null);var node=layer&&this._treeOutline.getCachedTreeElement(layer);if(node)
node.revealAndSelect(true);else if(this._treeOutline.selectedTreeElement)
this._treeOutline.selectedTreeElement.deselect();},hoverLayer:function(layer)
{var node=layer&&this._treeOutline.getCachedTreeElement(layer);if(node===this._lastHoveredNode)
return;if(this._lastHoveredNode)
this._lastHoveredNode.setHovered(false);if(node)
node.setHovered(true);this._lastHoveredNode=node;},_update:function()
{var seenLayers={};function updateLayer(layer)
{var id=layer.id();if(seenLayers[id])
console.assert(false,"Duplicate layer id: "+id);seenLayers[id]=true;var node=this._treeOutline.getCachedTreeElement(layer);var parent=layer===this._model.contentRoot()?this._treeOutline:this._treeOutline.getCachedTreeElement(layer.parent());if(!parent)
console.assert(false,"Parent is not in the tree");if(!node){node=new WebInspector.LayerTreeElement(this,layer);parent.appendChild(node);}else{if(node.parent!==parent){node.parent.removeChild(node);parent.appendChild(node);}
node._update();}}
if(this._model.contentRoot())
this._model.forEachLayer(updateLayer.bind(this),this._model.contentRoot());for(var node=(this._treeOutline.children[0]);node&&!node.root;){if(seenLayers[node.representedObject.id()]){node=node.traverseNextTreeElement(false);}else{var nextNode=node.nextSibling||node.parent;node.parent.removeChild(node);if(node===this._lastHoveredNode)
this._lastHoveredNode=null;node=nextNode;}}},_onMouseMove:function(event)
{var node=this._treeOutline.treeElementFromPoint(event.pageX,event.pageY);if(node===this._lastHoveredNode)
return;this.dispatchEventToListeners(WebInspector.LayerTree.Events.LayerHovered,node&&node.representedObject);},_selectedNodeChanged:function(node)
{var layer=(node.representedObject);this.dispatchEventToListeners(WebInspector.LayerTree.Events.LayerSelected,layer);},_onContextMenu:function(event)
{var node=this._treeOutline.treeElementFromPoint(event.pageX,event.pageY);if(!node||!node.representedObject)
return;var layer=(node.representedObject);if(!layer)
return;var nodeId=layer.nodeIdForSelfOrAncestor();if(!nodeId)
return;var domNode=WebInspector.domModel.nodeForId(nodeId);if(!domNode)
return;var contextMenu=new WebInspector.ContextMenu(event);contextMenu.appendApplicableItems(domNode);contextMenu.show();},__proto__:WebInspector.Object.prototype}
WebInspector.LayerTreeElement=function(tree,layer)
{TreeElement.call(this,"",layer);this._layerTree=tree;this._update();}
WebInspector.LayerTreeElement.prototype={onattach:function()
{var selection=document.createElement("div");selection.className="selection";this.listItemElement.insertBefore(selection,this.listItemElement.firstChild);},_update:function()
{var layer=(this.representedObject);var nodeId=layer.nodeIdForSelfOrAncestor();var node=nodeId?WebInspector.domModel.nodeForId(nodeId):null;var title=document.createDocumentFragment();title.createChild("div","selection");title.appendChild(document.createTextNode(node?WebInspector.DOMPresentationUtils.simpleSelector(node):"#"+layer.id()));var details=title.createChild("span","dimmed");details.textContent=WebInspector.UIString(" (%d × %d)",layer.width(),layer.height());this.title=title;},onselect:function()
{this._layerTree._selectedNodeChanged(this);return false;},setHovered:function(hovered)
{this.listItemElement.classList.toggle("hovered",hovered);},__proto__:TreeElement.prototype};WebInspector.Layers3DView=function(model)
{WebInspector.VBox.call(this);this.element.classList.add("layers-3d-view");this._emptyView=new WebInspector.EmptyView(WebInspector.UIString("Not in the composited mode.\nConsider forcing composited mode in Settings."));this._model=model;this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerTreeChanged,this._update,this);this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerPainted,this._onLayerPainted,this);this._rotatingContainerElement=this.element.createChild("div","fill rotating-container");this._transformController=new WebInspector.TransformController(this.element);this._transformController.addEventListener(WebInspector.TransformController.Events.TransformChanged,this._onTransformChanged,this);this.element.addEventListener("dblclick",this._onDoubleClick.bind(this),false);this.element.addEventListener("click",this._onClick.bind(this),false);this.element.addEventListener("mouseout",this._onMouseMove.bind(this),false);this.element.addEventListener("mousemove",this._onMouseMove.bind(this),false);this.element.addEventListener("contextmenu",this._onContextMenu.bind(this),false);this._elementsByLayerId={};this._scaleAdjustmentStylesheet=this.element.ownerDocument.head.createChild("style");this._scaleAdjustmentStylesheet.disabled=true;this._lastOutlinedElement={};this._layerImage=document.createElement("img");this._layerImage.style.width="100%";this._layerImage.style.height="100%";WebInspector.settings.showPaintRects.addChangeListener(this._update,this);}
WebInspector.Layers3DView.OutlineType={Hovered:"hovered",Selected:"selected"}
WebInspector.Layers3DView.Events={LayerHovered:"LayerHovered",LayerSelected:"LayerSelected",LayerSnapshotRequested:"LayerSnapshotRequested"}
WebInspector.Layers3DView.PaintRectColors=[WebInspector.Color.fromRGBA([0,0x5F,0,0x3F]),WebInspector.Color.fromRGBA([0,0xAF,0,0x3F]),WebInspector.Color.fromRGBA([0,0xFF,0,0x3F])]
WebInspector.Layers3DView.ScrollRectTitles={RepaintsOnScroll:WebInspector.UIString("repaints on scroll"),TouchEventHandler:WebInspector.UIString("touch event listener"),WheelEventHandler:WebInspector.UIString("mousewheel event listener")}
WebInspector.Layers3DView.prototype={onResize:function()
{this._update();},willHide:function()
{this._scaleAdjustmentStylesheet.disabled=true;},wasShown:function()
{this._scaleAdjustmentStylesheet.disabled=false;if(this._needsUpdate)
this._update();},_setOutline:function(type,layer)
{var element=layer?this._elementForLayer(layer):null;var previousElement=this._lastOutlinedElement[type];if(previousElement===element)
return;this._lastOutlinedElement[type]=element;if(previousElement){previousElement.classList.remove(type);this._updateElementColor(previousElement);}
if(element){element.classList.add(type);this._updateElementColor(element);}},hoverLayer:function(layer)
{this._setOutline(WebInspector.Layers3DView.OutlineType.Hovered,layer);},selectLayer:function(layer)
{this._setOutline(WebInspector.Layers3DView.OutlineType.Hovered,null);this._setOutline(WebInspector.Layers3DView.OutlineType.Selected,layer);},showImageForLayer:function(layer,imageURL)
{var element=this._elementForLayer(layer);this._layerImage.removeAttribute("src");if(imageURL)
this._layerImage.src=imageURL;element.appendChild(this._layerImage);},_scaleToFit:function()
{var root=this._model.contentRoot();if(!root)
return;const padding=40;var scaleX=this._clientWidth/(root.width()+2*padding);var scaleY=this._clientHeight/(root.height()+2*padding);var autoScale=Math.min(scaleX,scaleY);this._scale=autoScale*this._transformController.scale();this._paddingX=((this._clientWidth/autoScale-root.width())>>1)*this._scale;this._paddingY=((this._clientHeight/autoScale-root.height())>>1)*this._scale;const screenLayerSpacing=20;this._layerSpacing=screenLayerSpacing+"px";const screenLayerThickness=4;var layerThickness=screenLayerThickness+"px";var stylesheetContent=".layer-container .side-wall { height: "+layerThickness+"; width: "+layerThickness+"; } "+".layer-container .back-wall { -webkit-transform: translateZ(-"+layerThickness+"); } "+".layer-container { -webkit-transform: translateZ("+this._layerSpacing+"); }";var stylesheetTextNode=this._scaleAdjustmentStylesheet.firstChild;if(!stylesheetTextNode||stylesheetTextNode.nodeType!==Node.TEXT_NODE||stylesheetTextNode.nextSibling)
this._scaleAdjustmentStylesheet.textContent=stylesheetContent;else
stylesheetTextNode.nodeValue=stylesheetContent;var style=this._elementForLayer(root).style;style.left=Math.round(this._paddingX)+"px";style.top=Math.round(this._paddingY)+"px";style.webkitTransformOrigin="";},_onTransformChanged:function(event)
{var changedTransforms=(event.data);if(changedTransforms&WebInspector.TransformController.TransformType.Scale)
this._update();else
this._updateTransform();},_updateTransform:function()
{var root=this._model.contentRoot();if(!root)
return;var offsetX=this._transformController.offsetX();var offsetY=this._transformController.offsetY();var style=this._rotatingContainerElement.style;style.webkitTransform="translateZ(10000px)"+" rotateX("+this._transformController.rotateX()+"deg) rotateY("+this._transformController.rotateY()+"deg)"+" translateX("+offsetX+"px) translateY("+offsetY+"px)";style.webkitTransformOrigin=Math.round(this._paddingX+offsetX+root.width()*this._scale/2)+"px "+Math.round(this._paddingY+offsetY+root.height()*this._scale/2)+"px";},_createScrollRectElement:function(layer)
{var element=document.createElement("div");var parentLayerElement=this._elementsByLayerId[layer.id()];element.className="scroll-rect";parentLayerElement.appendChild(element);return element;},_updateScrollRectElement:function(rect,element)
{var style=element.style;style.width=Math.round(rect.rect.width*this._scale)+"px";style.height=Math.round(rect.rect.height*this._scale)+"px";style.left=Math.round(rect.rect.x*this._scale)+"px";style.top=Math.round(rect.rect.y*this._scale)+"px";element.title=WebInspector.Layers3DView.ScrollRectTitles[rect.type];},_updateScrollRectsForLayer:function(layer)
{var layerDetails=this._elementsByLayerId[layer.id()].__layerDetails;function removeElement(element)
{element.remove()}
if(layer.scrollRects().length!==layerDetails.scrollRectElements.length){layerDetails.scrollRectElements.forEach(removeElement);layerDetails.scrollRectElements=layer.scrollRects().map(this._createScrollRectElement.bind(this,layer));}
for(var i=0;i<layer.scrollRects().length;++i)
this._updateScrollRectElement(layer.scrollRects()[i],layerDetails.scrollRectElements[i]);},_update:function()
{if(!this.isShowing()){this._needsUpdate=true;return;}
if(!this._model.contentRoot()){this._emptyView.show(this.element);this._rotatingContainerElement.removeChildren();return;}
this._emptyView.detach();function updateLayer(layer)
{this._updateLayerElement(this._elementForLayer(layer));this._updateScrollRectsForLayer(layer);}
this._clientWidth=this.element.clientWidth;this._clientHeight=this.element.clientHeight;for(var layerId in this._elementsByLayerId){if(this._model.layerById(layerId))
continue;this._elementsByLayerId[layerId].remove();delete this._elementsByLayerId[layerId];}
this._scaleToFit();this._updateTransform();this._model.forEachLayer(updateLayer.bind(this));this._needsUpdate=false;},_onLayerPainted:function(event)
{var layer=(event.data);this._updatePaintRect(this._elementForLayer(layer));},_elementForLayer:function(layer)
{var element=this._elementsByLayerId[layer.id()];if(element){element.__layerDetails.layer=layer;return element;}
element=document.createElement("div");element.__layerDetails=new WebInspector.LayerDetails(layer,element.createChild("div","paint-rect"));["fill back-wall","side-wall top","side-wall right","side-wall bottom","side-wall left"].forEach(element.createChild.bind(element,"div"));this._elementsByLayerId[layer.id()]=element;return element;},_updateLayerElement:function(element)
{var layer=element.__layerDetails.layer;var style=element.style;var contentRoot=(this._model.contentRoot());var isContentRoot=layer===contentRoot;var isRoot=layer===this._model.root();var parentElement;if(isContentRoot){parentElement=this._rotatingContainerElement;element.__layerDetails.depth=0;}else if(isRoot){parentElement=this._elementForLayer(contentRoot);element.__layerDetails.depth=undefined;}else{parentElement=this._elementForLayer(layer.parent());element.__layerDetails.depth=parentElement.__layerDetails.isAboveContentRoot()?undefined:parentElement.__layerDetails.depth+1;}
if(!element.__layerDetails.isAboveContentRoot())
element.className="layer-container";else
element.className="layer-transparent";element.classList.toggle("invisible",layer.invisible());this._updateElementColor(element);if(parentElement!==element.parentElement)
parentElement.appendChild(element);style.width=Math.round(layer.width()*this._scale)+"px";style.height=Math.round(layer.height()*this._scale)+"px";this._updatePaintRect(element);if(isContentRoot||isRoot)
return;style.left=Math.round(layer.offsetX()*this._scale)+"px";style.top=Math.round(layer.offsetY()*this._scale)+"px";var transform=layer.transform();if(transform){transform=transform.slice();for(var i=12;i<15;++i)
transform[i]*=this._scale;style.webkitTransform="matrix3d("+transform.map(toFixed5).join(",")+") translateZ("+this._layerSpacing+")";var anchor=layer.anchorPoint();style.webkitTransformOrigin=Math.round(anchor[0]*100)+"% "+Math.round(anchor[1]*100)+"% "+anchor[2];}else{style.webkitTransform="";style.webkitTransformOrigin="";}
function toFixed5(x)
{return x.toFixed(5);}},_updatePaintRect:function(element)
{var details=element.__layerDetails;var paintRect=details.layer.lastPaintRect();var paintRectElement=details.paintRectElement;if(!paintRect||!WebInspector.settings.showPaintRects.get()){paintRectElement.classList.add("hidden");return;}
paintRectElement.classList.remove("hidden");if(details.paintCount===details.layer.paintCount())
return;details.paintCount=details.layer.paintCount();var style=paintRectElement.style;style.left=Math.round(paintRect.x*this._scale)+"px";style.top=Math.round(paintRect.y*this._scale)+"px";style.width=Math.round(paintRect.width*this._scale)+"px";style.height=Math.round(paintRect.height*this._scale)+"px";var color=WebInspector.Layers3DView.PaintRectColors[details.paintCount%WebInspector.Layers3DView.PaintRectColors.length];style.borderWidth=Math.ceil(1/this._scale)+"px";style.borderColor=color.toString(WebInspector.Color.Format.RGBA);},_updateElementColor:function(element)
{var color;if(element===this._lastOutlinedElement[WebInspector.Layers3DView.OutlineType.Selected])
color=WebInspector.Color.PageHighlight.Content.toString(WebInspector.Color.Format.RGBA)||"";else{const base=144;var component=base+20*((element.__layerDetails.depth-1)%5);color="rgba("+component+","+component+","+component+", 0.8)";}
element.style.backgroundColor=color;},_layerFromEventPoint:function(event)
{var element=this.element.ownerDocument.elementFromPoint(event.pageX,event.pageY);if(!element)
return null;element=element.enclosingNodeOrSelfWithClass("layer-container");return element&&element.__layerDetails&&element.__layerDetails.layer;},_onContextMenu:function(event)
{var layer=this._layerFromEventPoint(event);var nodeId=layer&&layer.nodeIdForSelfOrAncestor();if(!nodeId)
return;var domNode=WebInspector.domModel.nodeForId(nodeId);if(!domNode)
return;var contextMenu=new WebInspector.ContextMenu(event);contextMenu.appendApplicableItems(domNode);contextMenu.show();},_onMouseMove:function(event)
{if(event.which)
return;this.dispatchEventToListeners(WebInspector.Layers3DView.Events.LayerHovered,this._layerFromEventPoint(event));},_onClick:function(event)
{this.dispatchEventToListeners(WebInspector.Layers3DView.Events.LayerSelected,this._layerFromEventPoint(event));},_onDoubleClick:function(event)
{var layer=this._layerFromEventPoint(event);if(layer)
this.dispatchEventToListeners(WebInspector.Layers3DView.Events.LayerSnapshotRequested,layer);event.stopPropagation();},__proto__:WebInspector.VBox.prototype}
WebInspector.LayerDetails=function(layer,paintRectElement)
{this.layer=layer;this.depth=0;this.paintRectElement=paintRectElement;this.paintCount=0;this.scrollRectElements=[];}
WebInspector.LayerDetails.prototype={isAboveContentRoot:function()
{return this.depth===undefined;}};WebInspector.LayerDetailsView=function(model)
{WebInspector.VBox.call(this);this.element.classList.add("layer-details-view");this._emptyView=new WebInspector.EmptyView(WebInspector.UIString("Select a layer to see its details"));this._createTable();this._model=model;this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerTreeChanged,this._onLayerTreeUpdated,this);this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerPainted,this._onLayerPainted,this);}
WebInspector.LayerDetailsView.CompositingReasonDetail={"transform3D":WebInspector.UIString("Composition due to association with an element with a CSS 3D transform."),"video":WebInspector.UIString("Composition due to association with a <video> element."),"canvas":WebInspector.UIString("Composition due to the element being a <canvas> element."),"plugin":WebInspector.UIString("Composition due to association with a plugin."),"iFrame":WebInspector.UIString("Composition due to association with an <iframe> element."),"backfaceVisibilityHidden":WebInspector.UIString("Composition due to association with an element with a \"backface-visibility: hidden\" style."),"animation":WebInspector.UIString("Composition due to association with an animated element."),"filters":WebInspector.UIString("Composition due to association with an element with CSS filters applied."),"positionFixed":WebInspector.UIString("Composition due to association with an element with a \"position: fixed\" style."),"positionSticky":WebInspector.UIString("Composition due to association with an element with a \"position: sticky\" style."),"overflowScrollingTouch":WebInspector.UIString("Composition due to association with an element with a \"overflow-scrolling: touch\" style."),"blending":WebInspector.UIString("Composition due to association with an element that has blend mode other than \"normal\"."),"assumedOverlap":WebInspector.UIString("Composition due to association with an element that may overlap other composited elements."),"overlap":WebInspector.UIString("Composition due to association with an element overlapping other composited elements."),"negativeZIndexChildren":WebInspector.UIString("Composition due to association with an element with descendants that have a negative z-index."),"transformWithCompositedDescendants":WebInspector.UIString("Composition due to association with an element with composited descendants."),"opacityWithCompositedDescendants":WebInspector.UIString("Composition due to association with an element with opacity applied and composited descendants."),"maskWithCompositedDescendants":WebInspector.UIString("Composition due to association with a masked element and composited descendants."),"reflectionWithCompositedDescendants":WebInspector.UIString("Composition due to association with an element with a reflection and composited descendants."),"filterWithCompositedDescendants":WebInspector.UIString("Composition due to association with an element with CSS filters applied and composited descendants."),"blendingWithCompositedDescendants":WebInspector.UIString("Composition due to association with an element with CSS blending applied and composited descendants."),"clipsCompositingDescendants":WebInspector.UIString("Composition due to association with an element clipping compositing descendants."),"perspective":WebInspector.UIString("Composition due to association with an element with perspective applied."),"preserve3D":WebInspector.UIString("Composition due to association with an element with a \"transform-style: preserve-3d\" style."),"root":WebInspector.UIString("Root layer."),"layerForClip":WebInspector.UIString("Layer for clip."),"layerForScrollbar":WebInspector.UIString("Layer for scrollbar."),"layerForScrollingContainer":WebInspector.UIString("Layer for scrolling container."),"layerForForeground":WebInspector.UIString("Layer for foreground."),"layerForBackground":WebInspector.UIString("Layer for background."),"layerForMask":WebInspector.UIString("Layer for mask."),"layerForVideoOverlay":WebInspector.UIString("Layer for video overlay.")};WebInspector.LayerDetailsView.prototype={setLayer:function(layer)
{this._layer=layer;if(this.isShowing())
this._update();},wasShown:function()
{WebInspector.View.prototype.wasShown.call(this);this._update();},_onLayerTreeUpdated:function()
{if(this.isShowing())
this._update();},_onLayerPainted:function(event)
{var layer=(event.data);if(this._layer===layer)
this._paintCountCell.textContent=layer.paintCount();},_update:function()
{if(!this._layer){this._tableElement.remove();this._emptyView.show(this.element);return;}
this._emptyView.detach();this.element.appendChild(this._tableElement);this._positionCell.textContent=WebInspector.UIString("%d,%d",this._layer.offsetX(),this._layer.offsetY());this._sizeCell.textContent=WebInspector.UIString("%d × %d",this._layer.width(),this._layer.height());this._paintCountCell.textContent=this._layer.paintCount();const bytesPerPixel=4;this._memoryEstimateCell.textContent=Number.bytesToString(this._layer.invisible()?0:this._layer.width()*this._layer.height()*bytesPerPixel);this._layer.requestCompositingReasons(this._updateCompositingReasons.bind(this));},_createTable:function()
{this._tableElement=this.element.createChild("table");this._tbodyElement=this._tableElement.createChild("tbody");this._positionCell=this._createRow(WebInspector.UIString("Position in parent:"));this._sizeCell=this._createRow(WebInspector.UIString("Size:"));this._compositingReasonsCell=this._createRow(WebInspector.UIString("Compositing Reasons:"));this._memoryEstimateCell=this._createRow(WebInspector.UIString("Memory estimate:"));this._paintCountCell=this._createRow(WebInspector.UIString("Paint count:"));},_createRow:function(title)
{var tr=this._tbodyElement.createChild("tr");var titleCell=tr.createChild("td");titleCell.textContent=title;return tr.createChild("td");},_updateCompositingReasons:function(compositingReasons)
{if(!compositingReasons||!compositingReasons.length){this._compositingReasonsCell.textContent="n/a";return;}
var fragment=document.createDocumentFragment();for(var i=0;i<compositingReasons.length;++i){if(i)
fragment.appendChild(document.createTextNode(","));var span=document.createElement("span");span.title=WebInspector.LayerDetailsView.CompositingReasonDetail[compositingReasons[i]]||"";span.textContent=compositingReasons[i];fragment.appendChild(span);}
this._compositingReasonsCell.removeChildren();this._compositingReasonsCell.appendChild(fragment);},__proto__:WebInspector.VBox.prototype};WebInspector.PaintProfilerView=function(model,layers3DView)
{WebInspector.VBox.call(this);this.element.classList.add("paint-profiler-view");this._model=model;this._layers3DView=layers3DView;this._canvas=this.element.createChild("canvas","fill");this._context=this._canvas.getContext("2d");this._selectionWindow=new WebInspector.OverviewGrid.Window(this.element,this.element);this._selectionWindow.addEventListener(WebInspector.OverviewGrid.Events.WindowChanged,this._onWindowChanged,this);this._innerBarWidth=4*window.devicePixelRatio;this._minBarHeight=4*window.devicePixelRatio;this._barPaddingWidth=2*window.devicePixelRatio;this._outerBarWidth=this._innerBarWidth+this._barPaddingWidth;this._reset();}
WebInspector.PaintProfilerView.prototype={onResize:function()
{this._update();},_update:function()
{this._canvas.width=this.element.clientWidth*window.devicePixelRatio;this._canvas.height=this.element.clientHeight*window.devicePixelRatio;this._samplesPerBar=0;if(!this._profiles||!this._profiles.length)
return;var maxBars=Math.floor((this._canvas.width-2*this._barPaddingWidth)/this._outerBarWidth);var sampleCount=this._profiles[0].length;this._samplesPerBar=Math.ceil(sampleCount/maxBars);var barCount=Math.floor(sampleCount/this._samplesPerBar);var maxBarTime=0;var barTimes=[];for(var i=0,lastBarIndex=0,lastBarTime=0;i<sampleCount;){for(var row=0;row<this._profiles.length;row++)
lastBarTime+=this._profiles[row][i];++i;if(i-lastBarIndex==this._samplesPerBar||i==sampleCount){lastBarTime/=this._profiles.length*(i-lastBarIndex);barTimes.push(lastBarTime);if(lastBarTime>maxBarTime)
maxBarTime=lastBarTime;lastBarTime=0;lastBarIndex=i;}}
const paddingHeight=4*window.devicePixelRatio;var scale=(this._canvas.height-paddingHeight-this._minBarHeight)/maxBarTime;this._context.fillStyle="rgba(110, 180, 110, 0.7)";for(var i=0;i<barTimes.length;++i)
this._renderBar(i,barTimes[i]*scale+this._minBarHeight);},_renderBar:function(index,height)
{var x=this._barPaddingWidth+index*this._outerBarWidth;var y=this._canvas.height-height;this._context.fillRect(x,y,this._innerBarWidth,height);},_onWindowChanged:function()
{if(this._updateImageTimer)
return;this._updateImageTimer=setTimeout(this._updateImage.bind(this),100);},_updateImage:function()
{delete this._updateImageTimer;if(!this._profiles||!this._profiles.length)
return;var screenLeft=this._selectionWindow.windowLeft*this._canvas.width;var screenRight=this._selectionWindow.windowRight*this._canvas.width;var barLeft=Math.floor((screenLeft-this._barPaddingWidth)/this._outerBarWidth);var barRight=Math.floor((screenRight-this._barPaddingWidth+this._innerBarWidth)/this._outerBarWidth);var stepLeft=Math.max(0,barLeft*this._samplesPerBar);var stepRight=Math.min(barRight*this._samplesPerBar,this._profiles[0].length);this._snapshot.requestImage(stepLeft,stepRight,this._layers3DView.showImageForLayer.bind(this._layers3DView,this._layer));},_reset:function()
{this._snapshot=null;this._profiles=null;this._selectionWindow.reset();},profile:function(layer)
{this._reset();this._layer=layer;this._layer.requestSnapshot(onSnapshotDone.bind(this));function onSnapshotDone(snapshot)
{this._snapshot=snapshot;if(!snapshot){this._profiles=null;this._update();return;}
snapshot.requestImage(null,null,this._layers3DView.showImageForLayer.bind(this._layers3DView,this._layer));snapshot.profile(onProfileDone.bind(this));}
function onProfileDone(profiles)
{this._profiles=profiles;this._update();}},__proto__:WebInspector.VBox.prototype};;WebInspector.TransformController=function(element)
{this.element=element;element.addEventListener("mousemove",this._onMouseMove.bind(this),false);element.addEventListener("mousedown",this._onMouseDown.bind(this),false);element.addEventListener("mouseup",this._onMouseUp.bind(this),false);element.addEventListener("mousewheel",this._onMouseWheel.bind(this),false);this.reset();}
WebInspector.TransformController.Events={TransformChanged:"TransformChanged"}
WebInspector.TransformController.TransformType={Offset:1<<0,Scale:1<<1,Rotation:1<<2}
WebInspector.TransformController.prototype={_postChangeEvent:function(changeType)
{this.dispatchEventToListeners(WebInspector.TransformController.Events.TransformChanged,changeType);},_onMouseMove:function(event)
{if(event.which!==1)
return;if(typeof this._originX!=="number")
this._setReferencePoint(event);this._rotateX=this._oldRotateX+(this._originY-event.clientY)/2;this._rotateY=this._oldRotateY-(this._originX-event.clientX)/4;this._postChangeEvent(WebInspector.TransformController.TransformType.Rotation);},reset:function()
{this._scale=1;this._offsetX=0;this._offsetY=0;this._rotateX=0;this._rotateY=0;},scale:function()
{return this._scale;},offsetX:function()
{return this._offsetX;},offsetY:function()
{return this._offsetY;},rotateX:function()
{return this._rotateX;},rotateY:function()
{return this._rotateY;},_onMouseWheel:function(event)
{if(event.shiftKey){const zoomFactor=1.1;const mouseWheelZoomSpeed=1/120;var scaleFactor=Math.pow(zoomFactor,event.wheelDeltaY*mouseWheelZoomSpeed);this._scale*=scaleFactor;this._offsetX-=(event.clientX-this.element.totalOffsetLeft()-this._offsetX)*(scaleFactor-1);this._offsetY-=(event.clientY-this.element.totalOffsetTop()-this._offsetY)*(scaleFactor-1);this._postChangeEvent(WebInspector.TransformController.TransformType.Scale|WebInspector.TransformController.TransformType.Offset);}else{this._offsetX+=event.wheelDeltaX;this._offsetY+=event.wheelDeltaY;this._postChangeEvent(WebInspector.TransformController.TransformType.Offset);}},_setReferencePoint:function(event)
{this._originX=event.clientX;this._originY=event.clientY;this._oldRotateX=this._rotateX;this._oldRotateY=this._rotateY;},_resetReferencePoint:function()
{delete this._originX;delete this._originY;delete this._oldRotateX;delete this._oldRotateY;},_onMouseDown:function(event)
{if(event.which!==1)
return;this._setReferencePoint(event);},_onMouseUp:function(event)
{if(event.which!==1)
return;this._resetReferencePoint();},__proto__:WebInspector.Object.prototype};WebInspector.LayersPanel=function()
{WebInspector.PanelWithSidebarTree.call(this,"layers",225);this.registerRequiredCSS("layersPanel.css");this.sidebarElement().classList.add("outline-disclosure");this.sidebarTree.element.classList.remove("sidebar-tree");this._model=new WebInspector.LayerTreeModel();this._model.addEventListener(WebInspector.LayerTreeModel.Events.LayerTreeChanged,this._onLayerTreeUpdated,this);this._currentlySelectedLayer=null;this._currentlyHoveredLayer=null;this._layerTree=new WebInspector.LayerTree(this._model,this.sidebarTree);this._layerTree.addEventListener(WebInspector.LayerTree.Events.LayerSelected,this._onLayerSelected,this);this._layerTree.addEventListener(WebInspector.LayerTree.Events.LayerHovered,this._onLayerHovered,this);this._rightSplitView=new WebInspector.SplitView(false,true,"layerDetailsSplitViewState");this._rightSplitView.show(this.mainElement());this._layers3DView=new WebInspector.Layers3DView(this._model);this._layers3DView.show(this._rightSplitView.mainElement());this._layers3DView.addEventListener(WebInspector.Layers3DView.Events.LayerSelected,this._onLayerSelected,this);this._layers3DView.addEventListener(WebInspector.Layers3DView.Events.LayerHovered,this._onLayerHovered,this);this._layers3DView.addEventListener(WebInspector.Layers3DView.Events.LayerSnapshotRequested,this._onSnapshotRequested,this);this._tabbedPane=new WebInspector.TabbedPane();this._tabbedPane.show(this._rightSplitView.sidebarElement());this._layerDetailsView=new WebInspector.LayerDetailsView(this._model);this._tabbedPane.appendTab(WebInspector.LayersPanel.DetailsViewTabs.Details,WebInspector.UIString("Details"),this._layerDetailsView);this._paintProfilerView=new WebInspector.PaintProfilerView(this._model,this._layers3DView);this._tabbedPane.appendTab(WebInspector.LayersPanel.DetailsViewTabs.Profiler,WebInspector.UIString("Profiler"),this._paintProfilerView);}
WebInspector.LayersPanel.DetailsViewTabs={Details:"details",Profiler:"profiler"};WebInspector.LayersPanel.prototype={wasShown:function()
{WebInspector.Panel.prototype.wasShown.call(this);this.sidebarTree.element.focus();this._model.enable();},willHide:function()
{this._model.disable();WebInspector.Panel.prototype.willHide.call(this);},_showSnapshot:function(snapshot)
{this._model.setSnapshot(snapshot);},_onLayerTreeUpdated:function()
{if(this._currentlySelectedLayer&&!this._model.layerById(this._currentlySelectedLayer.id()))
this._selectLayer(null);if(this._currentlyHoveredLayer&&!this._model.layerById(this._currentlyHoveredLayer.id()))
this._hoverLayer(null);},_onLayerSelected:function(event)
{var layer=(event.data);this._selectLayer(layer);},_onLayerHovered:function(event)
{var layer=(event.data);this._hoverLayer(layer);},_onSnapshotRequested:function(event)
{var layer=(event.data);this._tabbedPane.selectTab(WebInspector.LayersPanel.DetailsViewTabs.Profiler);this._paintProfilerView.profile(layer);},_selectLayer:function(layer)
{if(this._currentlySelectedLayer===layer)
return;this._currentlySelectedLayer=layer;var nodeId=layer&&layer.nodeIdForSelfOrAncestor();if(nodeId)
WebInspector.domModel.highlightDOMNodeForTwoSeconds(nodeId);else
WebInspector.domModel.hideDOMNodeHighlight();this._layerTree.selectLayer(layer);this._layers3DView.selectLayer(layer);this._layerDetailsView.setLayer(layer);},_hoverLayer:function(layer)
{if(this._currentlyHoveredLayer===layer)
return;this._currentlyHoveredLayer=layer;var nodeId=layer&&layer.nodeIdForSelfOrAncestor();if(nodeId)
WebInspector.domModel.highlightDOMNode(nodeId);else
WebInspector.domModel.hideDOMNodeHighlight();this._layerTree.hoverLayer(layer);this._layers3DView.hoverLayer(layer);},__proto__:WebInspector.PanelWithSidebarTree.prototype}
WebInspector.LayersPanel.LayerTreeRevealer=function()
{}
WebInspector.LayersPanel.LayerTreeRevealer.prototype={reveal:function(layerTree)
{if(layerTree instanceof WebInspector.LayerTreeSnapshot)
(WebInspector.inspectorView.showPanel("layers"))._showSnapshot(layerTree);}}