---
title: If
slug: /commands/if
parent: Commands
---

# If
The if command acts as a conditional goto command. If the input bool is true then the first string argument will be used as the name of the scope Kasic will go to. If false then the second string input will be the name of the scope Kasic will go to.
```
if [bool, string, string]
```
---
## Overview
```yaml
name: 'if'
min: 2
max: 3
arguments:
    - bool
    - string
    - string
return: void
```
---
## Example 
```
if true true_scope false_scope

> true_scope
print "The value was true"
goto finish

> false_scope
print "The value was false"
goto finish

> finish 
```
```
The value was true
```