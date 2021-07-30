---
title: Greater
slug: /commands/greater
parent: Commands
---

# Greater
The greater command is used to determine if a given number is greater then another
```
great [number, number]
```
---
## Overview
```yaml
name: 'great'
min: 2
max: 2
arguments:
    - number
    - number
return: bool
```
---
## Example 
```
>>> great 10 12
True
>>> great 100 5
False
>>> great 0 0
False
```