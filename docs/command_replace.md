---
title: Replace
slug: /commands/replace
parent: Commands
---

# Replace
The replace command can be used on strings to replace a given sub-string with another
```
replace -[i] [string, string, string]
```
---
## Overview
```yaml
name: 'replace'
min: 3
max: 3
arguments:
    - string
    - string
    - string
flags:
    - name: 'i'
      description: 'ignores the case of the string to be replaced'
return: string 
```
---
## Example 
```
>>> replace Hello Goodbye "Hello World"
Goodbye World
```