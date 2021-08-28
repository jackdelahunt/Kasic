---
title: Write
slug: /commands/write
parent: Commands
---

# Write
Writes data to a given file
```
write -[a] [string, any]
```
---
## Overview
```yaml
name: 'write'
min: 2
max: 2
flags:
    - name: 'a'
      description: 'appends to the file instead of overwiting'
arguments:
    - string
    - any
return: void
```
---
## Example 
```
>>> write "data" "Hello"
>>> exit
% cat data
Hello
```