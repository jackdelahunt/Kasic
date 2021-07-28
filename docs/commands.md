---
title: Commands
slug: /commands
nav_order: 2
has_children: true
---

# Commands
In Kasic almost all code wrote is invoking commands. In Kasic all commands adhear to the following structure.
```
command -[FLAGS] [Arguments]
```
---
## How to read the command docs
Each command in Kasic is documented bellow. In each description the command structure is layed-out as followed. Follow this example to understand what each property represents.

`name`: The word used to invoke the command<br>
`min`: The minimum number of arguments needed<br>
`max`: The maximum number of arguments accepted<br>
`arguments`: The types that each argument accepts<br>
`flags`: Arguments proceded by `-` to invoke different behaviuor<br>


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

description: 'replaces any occurance of the first string with the second within the third' 
```