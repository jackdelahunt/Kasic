---
title: Not
slug: /commands/not
parent: Commands
---

# Not
The not command returns the oppisite of the given bool
```
not [bool]
```
---
## Overview
```yaml
name: 'bool'
min: 1
max: 1
arguments:
    - bool
return: bool
```
---
## Example 
```
>>> not true
False
>>> not false
True
```