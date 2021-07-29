---
title: Pipes
slug: /pipes
---

# Pipes
Kasic uses pipes to pass the result of a command to the input of another. This functionallity allows for the programmer to create less temporary variables.  

---
## Overview
Everytime a pipe is used the result of that command is added as the last arguemt in the next command. Commands are executed from left to right with no context of the overall expression. 

When counting the number of arguments for a command piped arguments are included meaning using a command like `mult` with one argument would normally return an error but once an argument is piped the command executes as normal.
```
                             add 100 10 | mult 2
                                  |         |
                                 110  ->  mult 2
                                            |
                                        mult 2 110
                                            |
                                           220
```
---
## Example
```
>>> add 10 10 | string | replace 2 3 | num | add 10
40
```