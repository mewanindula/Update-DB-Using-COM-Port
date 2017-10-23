#include <DFRobot_sim808.h>
#include <SoftwareSerial.h>

#define MESSAGE_LENGTH 160
char message[MESSAGE_LENGTH];
char MESSAGE[200];
int messageIndex = 0;

char phone[16];

char datetime[24];

#define PIN_TX    10
#define PIN_RX    11

SoftwareSerial mySerial(PIN_TX,PIN_RX);
DFRobot_SIM808 sim808(&mySerial);//Connect RX,TX,PWR,

void setup()
{
 
  mySerial.begin(9600);
  Serial.begin(9600);
  
  pinMode(13,OUTPUT);
  digitalWrite(13,HIGH);

  //******** Initialize sim808 module *************
  while(!sim808.init())
  {
      Serial.print("Sim808 init error\r\n");
      delay(1000);
  }
  delay(1000);  

  Serial.println("SIM Init success");
      
  Serial.println("Init Success, please send SMS message to me!");

  digitalWrite(13,LOW);
}

void loop()
{
  //*********** Detecting unread SMS ************************
   messageIndex = sim808.isSMSunread();
   
   //*********** At least, there is one UNREAD SMS ***********
   if (messageIndex > 0)
   {
      digitalWrite(13,HIGH);
      
      sim808.readSMS(messageIndex, message, MESSAGE_LENGTH, phone, datetime);
                 
      //***********In order not to full SIM Memory, is better to delete it**********
      sim808.deleteSMS(messageIndex);

      sprintf(MESSAGE, "%s,%s,%s", message, datetime, phone);
     
      Serial.println(MESSAGE);

      digitalWrite(13,LOW);
   }
}
