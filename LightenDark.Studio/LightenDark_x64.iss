; prerequisities
#define use_dotnetfx46
#define use_msiproduct
#define use_vc2013

[Setup]

#define MyAppSetupName "LightenDark"
#define CompanyName "Lorenzo.cz"
#define MyAppVersion GetFileVersion(AddBackslash(SourcePath) + "bin\x64\Release\LightenDark.exe")

;AppID - unique ID!
AppId=8578b9fa-8c25-493c-9d7f-f0e294562804
AppName={#MyAppSetupName}
AppVersion={#MyAppVersion}
AppVerName={#MyAppSetupName}_{#MyAppVersion}
AppCopyright=Copyright © 2015 {#CompanyName}
;VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#CompanyName}
AppPublisher={#CompanyName}
AppPublisherURL=http://www.lightendark.lorenzo.cz
;AppSupportURL=http://...
;AppUpdatesURL=http://...
OutputBaseFilename={#MyAppSetupName}-{#MyAppVersion}x64
DefaultGroupName={#MyAppSetupName}
DefaultDirName={pf}\{#MyAppSetupName}
UninstallDisplayIcon={app}\LightenDark.exe
OutputDir=setup
SourceDir=.
AllowNoIcons=yes
;SetupIconFile=MyProgramIcon
SolidCompression=yes

;MinVersion default value: "0,5.0 (Windows 2000+) if Unicode Inno Setup, else 4.0,4.0 (Windows 95+)"
;MinVersion=0,5.0
PrivilegesRequired=admin
ArchitecturesAllowed=x64 ia64
ArchitecturesInstallIn64BitMode=x64 ia64

;Downloading and installing dependencies will only work if the memo/ready page is enabled (default behaviour)
DisableReadyPage=no
DisableReadyMemo=no

; graphic in wizard
WizardImageFile=setup\images\SetupModern19.bmp
WizardSmallImageFile=setup\images\SetupModernSmall19.bmp

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl"
Name: "de"; MessagesFile: "compiler:Languages\German.isl"

[Tasks]
Name: desktopicon; Description: "Create the icon on desktop"; GroupDescription: Icons:

[Run]
Filename: "{app}\LightenDark.exe"; Description: "Run LightenDark"; Flags: postinstall nowait skipifsilent

[Files]
; AppData
Source: bin\x64\Release\LightenDark.exe; DestDir: {app}; DestName: LightenDark.exe; Flags: overwritereadonly ignoreversion
Source: bin\x64\Release\LightenDark.exe.config; DestDir: {app}; DestName: LightenDark.exe.config; Flags: overwritereadonly ignoreversion
Source: bin\x64\Release\*.dll; DestDir: {app}; Flags: overwritereadonly ignoreversion recursesubdirs
Source: bin\x64\Release\Data\*.*; DestDir: {app}\Data
; Cef
Source: bin\x64\Release\locales\*.*; DestDir: {app}\locales
Source: bin\x64\Release\*.pak; DestDir: {app}; Flags: overwritereadonly ignoreversion recursesubdirs
Source: bin\x64\Release\*.dat; DestDir: {app}; Flags: overwritereadonly ignoreversion recursesubdirs
Source: bin\x64\Release\natives_blob.bin; DestDir: {app}; DestName: natives_blob.bin; Flags: overwritereadonly ignoreversion
Source: bin\x64\Release\snapshot_blob.bin; DestDir: {app}; DestName: snapshot_blob.bin; Flags: overwritereadonly ignoreversion
Source: bin\x64\Release\CefSharp.BrowserSubprocess.exe; DestDir: {app}; DestName: CefSharp.BrowserSubprocess.exe; Flags: overwritereadonly ignoreversion

[Dirs]

[Icons]
Name: {group}\LightenDark; Filename: {app}\LightenDark.exe; WorkingDir: {app}
Name: {commondesktop}\LightenDark; Filename: {app}\LightenDark.exe; WorkingDir: {app}; Tasks: desktopicon

[Code]
// shared code for installing the products
#include "Setup\products.iss"
// helper functions
#include "Setup\products\stringversion.iss"
#include "Setup\products\winversion.iss"
#include "Setup\products\fileversion.iss"
#include "Setup\products\dotnetfxversion.iss"

// actual products
#ifdef use_iis
#include "Setup\products\iis.iss"
#endif

#ifdef use_kb835732
#include "Setup\products\kb835732.iss"
#endif

#ifdef use_msi20
#include "Setup\products\msi20.iss"
#endif
#ifdef use_msi31
#include "Setup\products\msi31.iss"
#endif
#ifdef use_msi45
#include "Setup\products\msi45.iss"
#endif

#ifdef use_ie6
#include "Setup\products\ie6.iss"
#endif

#ifdef use_dotnetfx11
#include "Setup\products\dotnetfx11.iss"
#include "Setup\products\dotnetfx11sp1.iss"
#ifdef use_dotnetfx11lp
#include "Setup\products\dotnetfx11lp.iss"
#endif
#endif

#ifdef use_dotnetfx20
#include "Setup\products\dotnetfx20.iss"
#include "Setup\products\dotnetfx20sp1.iss"
#include "Setup\products\dotnetfx20sp2.iss"
#ifdef use_dotnetfx20lp
#include "Setup\products\dotnetfx20lp.iss"
#include "Setup\products\dotnetfx20sp1lp.iss"
#include "Setup\products\dotnetfx20sp2lp.iss"
#endif
#endif

#ifdef use_dotnetfx35
//#include "Setup\products\dotnetfx35.iss"
#include "Setup\products\dotnetfx35sp1.iss"
#ifdef use_dotnetfx35lp
//#include "Setup\products\dotnetfx35lp.iss"
#include "Setup\products\dotnetfx35sp1lp.iss"
#endif
#endif

#ifdef use_dotnetfx40
#include "Setup\products\dotnetfx40client.iss"
#include "Setup\products\dotnetfx40full.iss"
#endif

#ifdef use_dotnetfx46
#include "Setup\products\dotnetfx46.iss"
#endif

#ifdef use_wic
#include "Setup\products\wic.iss"
#endif

#ifdef use_msiproduct
#include "Setup\products\msiproduct.iss"
#endif
#ifdef use_vc2005
#include "Setup\products\vcredist2005.iss"
#endif
#ifdef use_vc2008
#include "Setup\products\vcredist2008.iss"
#endif
#ifdef use_vc2010
#include "Setup\products\vcredist2010.iss"
#endif
#ifdef use_vc2012
#include "Setup\products\vcredist2012.iss"
#endif
#ifdef use_vc2013
#include "Setup\products\vcredist2013.iss"
#endif
#ifdef use_vc2015
#include "Setup\products\vcredist2015.iss"
#endif

#ifdef use_mdac28
#include "Setup\products\mdac28.iss"
#endif
#ifdef use_jet4sp8
#include "Setup\products\jet4sp8.iss"
#endif

#ifdef use_sqlcompact35sp2
#include "Setup\products\sqlcompact35sp2.iss"
#endif

#ifdef use_sql2005express
#include "Setup\products\sql2005express.iss"
#endif
#ifdef use_sql2008express
#include "Setup\products\sql2008express.iss"
#endif


function InitializeSetup(): boolean;
begin
	// initialize windows version
	initwinversion();

#ifdef use_iis
	if (not iis()) then exit;
#endif

#ifdef use_msi20
	msi20('2.0'); // min allowed version is 2.0
#endif
#ifdef use_msi31
	msi31('3.1'); // min allowed version is 3.1
#endif
#ifdef use_msi45
	msi45('4.5'); // min allowed version is 4.5
#endif
#ifdef use_ie6
	ie6('5.0.2919'); // min allowed version is 5.0.2919
#endif

#ifdef use_dotnetfx11
	dotnetfx11();
#ifdef use_dotnetfx11lp
	dotnetfx11lp();
#endif
	dotnetfx11sp1();
#endif

	// install .netfx 2.0 sp2 if possible; if not sp1 if possible; if not .netfx 2.0
#ifdef use_dotnetfx20
	// check if .netfx 2.0 can be installed on this OS
	if not minwinspversion(5, 0, 3) then begin
		msgbox(fmtmessage(custommessage('depinstall_missing'), [fmtmessage(custommessage('win_sp_title'), ['2000', '3'])]), mberror, mb_ok);
		exit;
	end;
	if not minwinspversion(5, 1, 2) then begin
		msgbox(fmtmessage(custommessage('depinstall_missing'), [fmtmessage(custommessage('win_sp_title'), ['XP', '2'])]), mberror, mb_ok);
		exit;
	end;

	if minwinversion(5, 1) then begin
		dotnetfx20sp2();
#ifdef use_dotnetfx20lp
		dotnetfx20sp2lp();
#endif
	end else begin
		if minwinversion(5, 0) and minwinspversion(5, 0, 4) then begin
#ifdef use_kb835732
			kb835732();
#endif
			dotnetfx20sp1();
#ifdef use_dotnetfx20lp
			dotnetfx20sp1lp();
#endif
		end else begin
			dotnetfx20();
#ifdef use_dotnetfx20lp
			dotnetfx20lp();
#endif
		end;
	end;
#endif

#ifdef use_dotnetfx35
	//dotnetfx35();
	dotnetfx35sp1();
#ifdef use_dotnetfx35lp
	//dotnetfx35lp();
	dotnetfx35sp1lp();
#endif
#endif

#ifdef use_wic
	wic();
#endif

	// if no .netfx 4.0 is found, install the client (smallest)
#ifdef use_dotnetfx40
	if (not netfxinstalled(NetFx40Client, '') and not netfxinstalled(NetFx40Full, '')) then
		dotnetfx40client();
#endif

#ifdef use_dotnetfx46
    dotnetfx46(50); // min allowed version is 4.5.0
#endif

#ifdef use_vc2005
	vcredist2005();
#endif
#ifdef use_vc2008
	vcredist2008();
#endif
#ifdef use_vc2010
	vcredist2010();
#endif
#ifdef use_vc2012
	vcredist2012();
#endif
#ifdef use_vc2013
	//SetForceX86(true); // force 32-bit install of next products
	vcredist2013();
	//SetForceX86(false); // disable forced 32-bit install again
#endif
#ifdef use_vc2015
	vcredist2015();
#endif

#ifdef use_mdac28
	mdac28('2.7'); // min allowed version is 2.7
#endif
#ifdef use_jet4sp8
	jet4sp8('4.0.8015'); // min allowed version is 4.0.8015
#endif

#ifdef use_sqlcompact35sp2
	sqlcompact35sp2();
#endif

#ifdef use_sql2005express
	sql2005express();
#endif
#ifdef use_sql2008express
	sql2008express();
#endif

	Result := true;
end; 