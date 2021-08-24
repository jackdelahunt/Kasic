---
title: Print
slug: /commands/print
parent: Commands
---

# Print
The print command prints any input to stdout with a space between each argument
```
print -[c] [any, any, any, any]
```
---
## Overview
```yaml
name: 'print'
min: 1
max: 4
flags:
    - name: 'c'
      description: 'concatenates each argument before printing'
    - name: 'l'
      description: 'stops print from appending a new line character to the end of the output'
arguments:
    - any
    - any
    - any
    - any
return: void
```
---
## Example 
```
>>> print "Hello World!"
Hello World!
>>> print -c "Hel" "lo " "Wor" "ld!"
Hello World!
```