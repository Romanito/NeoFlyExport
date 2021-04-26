# NeoFlyExport

Small program that reads [NeoFly](https://www.neofly.net/)'s logbook and exports it as a GPS trace in a GPX file.

## Usage

* Launch NeoFlyExpert.exe to use the interface mode.
* Launch NeoFlyExpert.exe with command line arguments to automate data export:
	
		--file         File name to export the data to
		--dateFrom     Exports data from this date and after
		--dateTo       Exports data from this date and before
		
		Ex:  NeoFlyExport.exe --file=Neofly.gpx --dateFrom=2021-04-01 --dateTo=2021-04-30

## External viewer

If an external viewer is set, traces can be viewed with it by double clicking them in the main window.
External viewers are set in the Settings window. Type the full path to the viewer's executable in the dedicated field.
Several viewers can be added. The first one in the list will be used by default.

Tested viewers:
* [Viking](https://sourceforge.net/projects/viking/)
* [GPXSee](https://www.gpxsee.org/)
* [GpsPrune](https://activityworkshop.net/software/gpsprune/)

## Resources

Project home page: https://github.com/Romanito/NeoFlyExport

## Credits

* NeoFlyExpert written by [Romanito](https://github.com/Romanito).
* Icon made by [Freepik](https://www.flaticon.com/authors/freepik).

## Licenses

NeoFlyExport is released under the [MIT licence](licence.txt).