---
title: StringRef
slug: /commands/string_ref
parent: Commands
---

# StringRef
The stringRef command is used to create new or change the value of variables with a given name with the passed string. To retrive the data in the variable the derefrence operator(`*`) is used. StringRef also returns the value passed to it.
```
&bool [string, string]
```
---
## Overview
```yaml
name: '&string'
min: 2
max: 2
arguments:
    - string
    - string
return: string
```
---
## Example 
```
>>> &string my_string "Hello World"
Hello World
>>> print *my_string
Hello World
>>> &string my_string "Goodbye World"
Goodbye World
>>> print *my_string
Goodbye World
```