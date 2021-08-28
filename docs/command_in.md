---
title: Input
slug: /commands/In
parent: Commands
---

# Input
The input command returns the input from the console. 
```
in -[l] [any]
```
---
## Overview
```yaml
name: 'in'
min: 0
max: 1
flags:
    - name: 'l'
      description: 'stops in from appending a new line character when printing annotation'
return: string
```
---
## Example 
```
>>> in | num | mult 100
10
1000
>>> in -l "Name: " | print "Your name is: "
Name: Billy Sheers
Your name is: Billy Sheers 
```