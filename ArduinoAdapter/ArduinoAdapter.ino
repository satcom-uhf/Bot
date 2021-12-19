void setup()
{
    int i = 5;
    while (i <= 11)
    {
        pinMode(i, OUTPUT);
        digitalWrite(i, HIGH);
        i++;
    }
    digitalWrite(8, LOW);
    Serial.begin(9600);
}

void click(int rowV, int colV, bool longClick)
{
    if (longClick)
        Serial.print("L ");
    Serial.print(rowV);
    Serial.print(colV);
    Serial.print("\r\n");
    bool levelR = rowV == 8;
    digitalWrite(rowV, levelR);
    digitalWrite(colV, LOW);
    if (longClick)
    {
        delay(2000);
    }
    else
    {
        delay(500);
    }
    digitalWrite(rowV, !levelR);
    digitalWrite(colV, HIGH);
}

void Check(String expected, String received, int row, int col)
{
    if (received.equalsIgnoreCase(expected))
    {
        click(row, col, received == expected);
    }
}

void loop()
{
    if (Serial.available())
    {
        String cmd = Serial.readString();
        Check("P2", cmd, 8, 5);
        Check("UP", cmd, 8, 6);
        Check("LEFT", cmd, 8, 7);
        Check("P1", cmd, 9, 5);
        Check("DOWN", cmd, 9, 6);
        Check("EXIT", cmd, 9, 7);
        Check("P4", cmd, 10, 5);
        Check("RIGHT", cmd, 10, 7);
        Check("P3", cmd, 11, 5);
        Check("OK", cmd, 11, 7);
    }
}