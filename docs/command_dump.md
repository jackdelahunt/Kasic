---
title: Dump
slug: /commands/dump
parent: Commands
---

# Dump
Traverses the heap and returns all data stored in JSON format.
```
&dump -[i] []
```
---
## Overview
```yaml
name: 'dump'
min: 0
max: 0
flags:
    - name: '-i'
      description: 'the JSON returned is formetted with indentation'
return: string
```
---
## Example 
```
>>> &bool falsey true
True
>>> dump
[{"ObjectId":0,"Name":"falsey","Type":3,"Data":true,"Const":false}]
```