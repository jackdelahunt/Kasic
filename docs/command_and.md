---
title: And
slug: /commands/And
parent: Commands
---

# And
The and command returns true if both inputs are true. 
```
and [bool, bool]
```
---
## Overview
```yaml
name: 'and'
min: 2
max: 2
arguments:
    - bool
    - bool
return: bool
```
---
## Example 
```
>>> and true true
True
>>> and false true
False
```