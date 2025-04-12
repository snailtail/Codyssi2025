# Laestrygonian Guards!
## Part 1

Difficulty Rating: 3
It has been a day since the struggle against Charybdis. It was rough, and one of your friends almost got flung overboard by a stray tentacle slamming against the hull, but your team and the captain’s crew made it out alive!

Now, you are nearly in position to descend to Atlantis!

There’s just one slight problem: there are many laestrygonians patrolling this region. You’ll have to sail in this region undetected until the coast is clear to make a dash to the drop zone!

Before heading into the region, your team members analyse the laestrygonians’ patrol shifts, identifying any locations that are not covered by the patrolling giants. After an impressive display of teamwork, a file of suitable paths between these ‘unseen’ locations is produced!

Each line in the file is formatted as “{start location} -> {end location} | {path length}” and represents a suitable path between two 'unseen' locations. Each path can only be travelled in one direction, and the number on each line is the length of the path represented by that line.

Your teammates inform you that the current location of the ship is represented by STT in the file.

Now, it is your turn.

For your warm-up, you’ll assume that each path has a length of 1. Using this assumption, find the length of the shortest paths from your current location to each location in the file. Of these paths, find the product of the lengths of the 3 longest paths.

For example, consider the shorter file of paths as an example:

STT -> MFP | 5  
AIB -> ZGK | 6  
ZGK -> KVX | 20  
STT -> AFG | 4  
AFG -> ZGK | 16  
MFP -> BDD | 13  
BDD -> AIB | 5  
AXU -> MFP | 4  
CLB -> BLV | 20  
AIB -> BDD | 13  
BLV -> AXU | 17  
AFG -> CLB | 2  


Assuming that each path has a length of 1, the lengths of the shortest paths from your current position, ‘STT’, to each location in the file are as follows:

STT to STT has a length of 0, by path STT.  
STT to MFP has a length of 1, by path STT -> MFP.   
STT to AFG has a length of 1, by path STT -> AFG.  
STT to BDD has a length of 2, by path STT -> MFP -> BDD.    
STT to ZGK has a length of 2, by path STT -> AFG -> ZGK.  
STT to CLB has a length of 2, by path STT -> AFG -> CLB.  
STT to KVX has a length of 3, by path STT -> AFG -> ZGK -> KVX.  
STT to AIB has a length of 3, by path STT -> MFP -> BDD -> AIB.  
STT to BLV has a length of 3, by path STT -> AFG -> CLB -> BLV.  
STT to AXU has a length of 4, by path STT -> AFG -> CLB -> BLV -> AXU.  

The product of the 3 longest path lengths is 4 x 3 x 3 = 36. Hence, the answer for this file would be 36.

Considering your file and using your assumption, find the shortest paths from your current location to each location in the file. Of these paths, what is the product of the lengths of the 3 longest paths?