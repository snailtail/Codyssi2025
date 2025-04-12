Artifacts at Atlantis!
Part 1

Difficulty Rating: 4
It’s been hours, and you haven’t seen any trace of Atlantis yet. How much longer will it be?

Wait… Could that be it? Yes!

A majestic golden castle materialises from the depths. There it is. The fabled castle of Atlantis! It looks more grandiose than you ever imagined…

Your team snaps out of their collective stupor as you remember the time crunch that you face. As you jump into your pressurised diving suits, you grab the extraction tools you crafted during your descent.

After an hour of careful artifact extraction from the ruins of the castle, you gather all the artifacts into one pile. Your team has already produced a list of artifacts. Each line in the list represents one artifact and consists of a 7-letter code and the artifact’s ID.

Now, you’ll have to keep the items in a storage mechanism. The storage mechanism in the submarine is a tree-based storage system. Each node in the system is connected to two ‘descendant’ nodes below it: one to its’ left and one to its’ right. Each node can store one artifact.

The root node is the node that starts the tree. The first artifact in the file is stored in the root node.

Then, consider every other artifact in the file, in file order. Use a comparison mechanism starting at the root node: if the ID of the current artifact is greater than the artifact stored in the root node, then it goes to the right descendant; otherwise, it goes to the left descendant. If the next node is empty, the artifact is stored at that node. Else, the comparison mechanism is repeated until an empty node is found to store the artifact.

The root node is considered to be in layer 1. The descendants of the root node are considered to be in layer 2. The descendants of those nodes are considered to be in layer 3, and so on.

The sum of a layer is the sum of the IDs of every artifact that is stored on that layer. A layer is considered to be an occupied layer if there is at least one artifact stored in a node on that layer.

After you store every artifact in the submarine’s storage system, multiply the maximum sum of a single layer by the number of occupied layers to get your answer.

For this task, you will ignore the final two lines in your file.

For example, consider the following list of artifacts, ignoring the final two lines:

ozNxANO | 576690
pYNonIG | 323352
MUantNm | 422646
lOSlxki | 548306
SDJtdpa | 493637
ocWkKQi | 747973
qfSKloT | 967749
KGRZQKg | 661714
JSXfNAJ | 499862
LnDiFPd | 55528
FyNcJHX | 9047
UfWSgzb | 200543
PtRtdSE | 314969
gwHsSzH | 960026
JoyLmZv | 833936

MUantNm | 422646
FyNcJHX | 9047


There are 15 artifacts in this list (the last two lines are ignored).
The first layer of the storage system will hold 1 artifact (ID 576690).
The second layer of the storage system will hold 2 artifacts (IDs 323352 and 747973).
The third layer of the storage system will hold 4 artifacts (IDs 55528, 422646, 661714, and 967749).
The fourth layer of the storage system will hold 4 artifacts (IDs 9047, 200543, 548306, and 960026).
The fifth layer of the storage system will hold 3 artifacts (IDs 314969, 493637, and 833936).
The sixth layer of the storage system will hold 1 artifact (ID 499862).

The third layer has a sum of 2107637, which is the largest sum of any layer. There are 6 occupied layers in total. The product of 6 and 2107637 is 12645822.

Hence, the answer for this file is 12645822.

Arrange the artifacts in your file into the submarine’s storage system. What is the product of the maximum sum of a single layer and the number of occupied layers?