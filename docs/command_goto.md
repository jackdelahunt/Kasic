---
title: Goto
slug: /commands/goto
parent: Commands
---

# Goto
Goto is used to move the point of execution in kasic. Goto takes a name of a defined scope and moves to that line in the code.
```
goto [string]
```
---
## Overview
```yaml
name: 'goto'
min: 1
max: 1
arguments:
    - string
return: void
```
---
## Example 
```
print Start
goto last

> middle
print Middle
goto finish 

> last
print Last
goto middle 

> finish
```
```
Start
Last
Middle
```