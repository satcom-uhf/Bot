#include <SoftwareSerial.h>
 
SoftwareSerial bus(2, 3);
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
    Serial.begin(115200);
    bus.begin(9600);
    bus.listen();
}

void click(int rowV, int colV, bool longClick)
{
    bool levelR = rowV == 8;
    digitalWrite(rowV, levelR);
    digitalWrite(colV, LOW);
    if (longClick)
    {
        delay(2000);
    }
    else
    {
        delay(250);
    }
    digitalWrite(rowV, !levelR);
    digitalWrite(colV, HIGH);
}

bool Check(String expected, String received, int row, int col)
{
    if (received.equalsIgnoreCase(expected))
    {
        click(row, col, received == expected);
        return true;
    }
    return false;
}
unsigned long time;
void loop()
{
  if (millis()-time>750){
    int sMeter=analogRead(A0);
    byte msg[4];
    msg[0]= 0x16;//smeter mark
    msg[1] = ((sMeter >> 8) & 0xff);//high byte
    msg[2]  = (sMeter & 0xff);//low byte
    msg[3]=0x50;
    Serial.write(msg, 4);
    time=millis();
    }
   while (bus.available()) {
    byte SBEP_byte = bus.read();
    Serial.write(SBEP_byte);
   }
    if (Serial.available())
    {
        String cmd = Serial.readString();
        Check("P2", cmd, 8, 5)
        || Check("UP", cmd, 8, 6)
        || Check("LEFT", cmd, 8, 7)
        || Check("P1", cmd, 9, 5)
        || Check("DOWN", cmd, 9, 6)
        || Check("EXIT", cmd, 9, 7)
        || Check("P4", cmd, 10, 5)
        || Check("RIGHT", cmd, 10, 7)
        || Check("P3", cmd, 11, 5)
        || Check("OK", cmd, 11, 7);
    }
}
