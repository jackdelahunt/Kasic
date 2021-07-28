---
title: NumRef
slug: /commands/num_ref
parent: Commands
---

# NumRef
The numRef command is used to create new or change the value of variables with a given name with the passed number. To retrive the data in the variable the derefrence operator(`*`) is used. NumRef also returns the value passed to it.
```
&num [string, number]
```
---
## Overview
```yaml
name: '&num'
min: 2
max: 2
arguments:
    - string
    - number
return: number
```
---
## Example 
```
>>> &num my_number 100
100
>>> print *my_number
100
>>> &num my_number 200
200
>>> print *my_number
200
```