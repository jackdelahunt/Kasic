# Kasic
Kasic is an interpretuer that trys to replicate the usage of Unix commands but as a programming language.

### Command Line
```bash
% kasic
>>> print "Hello world"
Hello World
```

### Headless
```bash
% kasic hello_word.kasic
Hello World
```

## Examples
See the [examples](https://github.com/jackdelahunt/Kasic/tree/main/kasic/Examples) directories for more comprehansive examples.

### Type inference
```bash
num number 10
string *number | replace 1 2 | num number
print *number
```
```bash
20
```
### Loop iteration
```bash
num number 0
string prompt "The number is:"

> loop
print *prompt *number
add *number 0.01 | num number
goto loop
```
```bash
0.01
0.02
0.03
...
```