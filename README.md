## Journal

### Saturday, December 27th

#### 11:00

Followed a bunch of videos on generating terrain. Now trying to get this player thing working and not falling off map. Also working on spawning trees.

### Sunday, December 28th

#### 15:00

Wrote a lot of logic to spawn trees. Then made it so it can be at a higher density than the mesh, but having a bit of issues with making follow the mesh height. Think I'll fix it tmrw, but I also smell issues with lag and needing scaling everything up, should probly do it porberly instead of using scaling.

### Monday, December 29th

#### 23:00

Got the trees to spawn in the right spots, but the height is off for some reason, will look into ts tmrw morning.

### Tuesday, December 30th

#### 16:30

Goal today is to write my own chunking system AND NOT USE ANY CLANKERS. To do this I am going to start by making the game take multiple meshes and renderthem together as multiple chunks. Then make it only active (i think thats the term) the chunks < x blocks from player.

#### 17:00

Started working on the chunking logic. Got to testing placing single chunks and its not working but im getting errors so not gonna be too hard to fix I hope. Will fix after dinner.

#### 20:00

![Working mesh](/readme_photos/working_mesh.png)

Got the chunking thing working a bit, now I need to write a function that takes the total map size and draws the meshes for it...

They are all aligned but have a weird y axis spacing diffrence issue, hopefully can fix in morning...

### Saturday, January 10th

#### 14:00

Fixed the chunk spawning issue, now time to work on the function that will only spawn chunks x units close to you.

Also might change this game from a survival game to a drone sim... Need to think about it a bit...

#### 16:00

Got seams at edges of chunks fixed. Also fixed tree spawning and made it per chunk.

Might want to change it so that it deletes each chunk when not near, not just disable. Idt disable does anything other than hide it.

Now need to work on drone physics.

#### 18:00

Going to now make it so trees don't spawn too close, then work on drone physics.

Done with tree spawning distance, now going to add a tree height offset, then work on drone stuff.

#### 19:00

Started working on the drone logic. Made a body, camera, and script.

## Credits

`"Low Poly Pine Tree" (https://skfb.ly/6WYVr) by skaljowsky is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).`
