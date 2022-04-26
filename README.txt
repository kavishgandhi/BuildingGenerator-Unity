Bulding generation
Click on the building GameObject
Click on the Generate Button-
    this will create 6 buildings in a diagonal way
    each of them will have a patch which is randomnly generated
    along with that we will have different heights for each cell
Change the Seed values to generate different configs
Click on Destroy All to Destroy all building cells
Windows, Roofs, Roof Overhangs, Doors, Walls - all are created using a random value generated using the Seed
Min seed value to see all different variants - 
    Seed for size = 15
    Seed for floor count = 10
Seed for size gives an uppper bound for width and height of building patches (lower bound is 3)
Seed for floor count gives the upper bound on floors
Different types of windows, roofs, doors are generated using random number, so it might be possible that in one building only one variant will come. But in other building the different variants will be present.
Also by repeatedly generating the buildings, it is possible thta you will be able to see all variants in every building.

You can check the debug output to see all different patches which are generated, it might be the case where 2 patches may look together as one, but they are different.
Prefabs are as follows -
Floor - Floor
Wall - Wall
Roof - Roof
Door - Door
Window - Window
Balcony - Balcony
Door Variant - Door Variant
Roof Variant - Roof Variant
Roof Overhang - Overhang Roof Variant
Dome - Dome