#!/bin/bash

cd ..
zip -r Droids_`cat Droids/VERSION`.zip \
  Droids/Assemblies/Droids.dll \
  Droids/Defs/ \
  Droids/Languages/ \
  Droids/Textures/ \
  Droids/README.md \
  Droids/src/
