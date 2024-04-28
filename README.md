# College Caper
 
WebGL: https://artofgaming.github.io/College-Caper/BuildMine/


This fashion game is a solo project that utilizes MVC architecture to create a simple dress up game experience. The view layer is inside of Unity, and all the clothes are kept inside a catalogue database as well as a list of their attributes. Each item has different emotion values based off of their attributes and styles.

<img width="506" alt="Screenshot 2024-04-27 123610" src="https://github.com/ArtofGaming/College-Caper/assets/54565543/a517f1e4-33e3-4b12-b3c1-c1cdb797fd72">

<img width="250" alt="Screenshot 2024-04-27 123727" src="https://github.com/ArtofGaming/College-Caper/assets/54565543/a6547785-bec6-4c57-828a-fd4febcff39f">

The meshes were made in Blender and are linked with the databse by names so that whenever a certain item is selected the model that shares its name has its mesh renderer enabled on the model. 

<img width="116" alt="Screenshot 2024-04-27 124206" src="https://github.com/ArtofGaming/College-Caper/assets/54565543/e1fa64c6-8236-4806-98e2-448d09c9fbfa">

The tabs on the side filter items by clothing category and only those are sortable in the application for now, but on the SQL side this is already possible. Meshes can be cleared with the clear button next to the model or the page can be refreshed. The filter feature works thanks to two main functions, one that reads the appropriate item names from the server and connects them to their models and generates a box for them to sit in front of and the other which looks at all the items that don't correspond with the appropriate filter and removes them and the box they're a child of.
