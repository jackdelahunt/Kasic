---
title: Scopes
slug: /scopes
---

# Scopes
Scopes are used to define points possible points that Kasic will execute from. Execution always starts from the first line but with a [goto](https://jackdelahunt.github.io/Kasic/command_goto.html) or [if](https://jackdelahunt.github.io/Kasic/command_if.html) command this can be altered.
## Define a scope
---

To define a scope in Kasic the `>` operator is used, the first work after this operator is known as the scope name, this name is case sensitive. Any text after the scope name is disregarded.

---
## Functionality
Scopes only define points in which the program can be moved to. They do not block the execution if they are hit organically, this means if scopes are defined in code but no [goto](https://jackdelahunt.github.io/Kasic/command_goto.html) or [if](https://jackdelahunt.github.io/Kasic/command_if.html) is used then the program executes as if the scopes are not defined.

---
## Example
Below are defined two scopes, because these scopes are never used the program is executed from top to bottom.
```
print Start

> scope_1
print 1

> scope_2
print 2
```
```
Start
1
2
```
With the addition of [goto](https://jackdelahunt.github.io/Kasic/command_goto.html) commands moves where Kasic is executing this can be shown below where the first scope is skipped. 
```
print Start
goto scope_2

> scope_1
print 1

> scope_2
print 2
```
```
Start
2
```