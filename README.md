# Kasic
Kasic is an interpretuer that trys to replicate the usage of Unix commands but as a programming language.

## Examples
See the [examples](https://github.com/jackdelahunt/Kasic/tree/main/kasic/Examples) directories for more comprehansive examples.

### Type inference
```bash
set number 10
string *number | replace 1 2 | set modified_number
num *modified_number | add 2 | print

>>> 22
```
### Loop iteration
```bash
set number 0
set prompt Number_Is:

> loop
print -c *prompt *number
num *number | add 1 | set number
goto loop

>>> 0
>>> 1
...
```

### Conditional logic
```bash
set number 11

num *number | great 10 | if greater
goto less

> greater
print the_number_is_greater
goto finish

> less
print the_number_is_less
goto finish

> finish

>>> the_number_is_greater
```