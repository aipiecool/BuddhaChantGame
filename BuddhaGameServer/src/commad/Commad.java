package commad;

import java.util.InputMismatchException;
import java.util.Scanner;

public abstract class Commad {

    abstract public String getCommad();
    abstract public String getSimpleCommad();
    abstract public String getHelpMessage();
    abstract public void process(Scanner parms) throws InputMismatchException;
}
