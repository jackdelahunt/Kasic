# Kasic
Kasic is an interpretuer that trys to replicate the usage of Unix commands but as a programming language.

### Install
Download latest release from github [here](https://github.com/jackdelahunt/Kasic/releases).

```bash
unzip kasic.zip && mv kasic ~/ 
```
```bash
export PATH="$HOME/kasic:$PATH"
```

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
&num number 10
string *number | replace 1 2 | num | &num number
print *number
```
```bash
20
```
### Loop iteration
```bash
&num number 0
&string prompt "The number is:"

> loop
print *prompt *number
add *number 0.01 | &num number
goto loop
```
```bash
The number is: 0.01
The number is: 0.02
The number is: 0.03
...
```
## Build

Prerequisites
- [.NET 5.0](https://dotnet.microsoft.com/download)
```bash
git clone git@github.com:jackdelahunt/Kasic.git

cd Kasic/kasic

dotnet run
```
## Speed Comparision
See [here](https://github.com/jackdelahunt/Kasic/tree/main/kasic/Examples/Speed) for the code for each language.



| Kasic          | Speed(ms) |
| -------------- | --------- |
| 2021-07-24     | 4820      |
| 2021-07-25     | 3560      |
| 2021-07-26     | 2970      |
| 2021-07-27     | 1760      |
| 2021-07-28     | 800       |
| 2021-07-29     | 560       |




### In comparision
| Language | Speed(ms) |
| -------- | --------- |
| Python   | 100       |
| Java     | 6         |
| Rust     | 1         |
| C++      | <1        |

### Million iterations per second for each language compared 
![Chart](Resources/speed_chart.png)
