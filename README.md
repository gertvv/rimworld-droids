RimWorld Droids
===============

Adds the ability to build autonomous support droids to RimWorld. Currently a
transport (hauling) droid only. To build the transport droid, you need to do
the following:

  - Research Machining
  - Research Droid Manufacturing
  - Build a Droid Assembly Table
  - Craft Droid Parts x4; costs 75 metal, requires crafting 7
  - Craft Transport Droid; costs 4 droid parts, requires crafting 10
  - Build a Droid Charging Station to charge and activate the Droid

Translations
------------

There is a Portuguese translation included with the mod. Due to limitations of
Alpha 4, RimWorld won't load it. To get it to load, copy the contents of
`Mods/Droids/Languages/pt-PT/DefInjected/` into
`Mods/Core/Languages/pt-PT/DefLinked/`. This shouldn't overwrite any of the
existing files as I've tried to use unique filenames.

Credits
-------

Code and definitions by Gert van Valkenhoef, with some copy/paste from the
RimWorld base game.

[Droid graphics](http://opengameart.org/content/robot-0) by the PlatForge
project and Hannah Cohan.

The droid manufacturing table graphic is simply a color-adjusted machining
table, and I ruined the blasting charge to make the charging station graphic.

Portuguese translation by [decomg](http://ludeon.com/forums/index.php?action=profile;u=5968).

Everything except content taken from RimWorld licensed CC-BY-SA 3.0.

Changelog
---------

Version 0.4 (in development):

  - New graphics thanks to Psyckosama!
  - Smarter charging behaviour:
      - Droids check whether the station is powered on and available
      - Droids will now try to charge at a nearby station before getting
        to the "emergency" 8% charge level
  - Lowered crafting level requirements
  - Added About.xml for the Mods menu
  - Bugfix: charging stations now only charge when actually powered on
  - Bugfix: Droids no longer die of starvation
  - Bugfix: charging station now properly requires research to build

Version 0.3 (2014-06-13):

  - Droids can now open doors.
  - Inactive droids can now be repaired by colonists.
  - Add Portuguese translation by decomg

Version 0.2 (2014-06-12):

  - Droids now have a 100Wd battery. They take 50W during operation so last 2
    days on a charge.
  - Added Droid Charging Station building, which takes 600W to charge the
    battery in ~8 hours. When not charging takes 6W.
  - Droids will seek out charging station when < 8% charged.
  - Colonists will haul Droids to the charging station when they're out of
    power.
  - Emergency shutdown when low on health. Currently useless as they can't be
    repaired.

Version 0.1 (2014-06-08):

  - Initial release.
  - Craft Droids using a special crafting table. You need to craft Droid parts
    first, and do the right research.
  - A transport droid that only does hauling jobs.

