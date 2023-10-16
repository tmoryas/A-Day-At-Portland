#include <Arduino.h>

int led[10] = {2,3,4,5,6,7,8,9,10,11};
String str = "";
void AllInput(){
  for(int i=0;i<10;i++){
    pinMode(led[i],INPUT);
  }
}

void CheckAllPin(){
  str = "";
  for(int i=0;i<10;i++){
    AllInput();
    pinMode(led[i],OUTPUT);
    digitalWrite(led[i],HIGH);
    delay(50);

    for(int j=0;j<10;j++){
      if(j!=i && digitalRead(led[j]) == HIGH){
        str += String(i)+ "!" + String(j) + "/";
      }
    }
    digitalWrite(led[i],LOW);
    delay(50);
  }
  delay(50);
}

void setup() {
  Serial.begin(9600);
}

void loop() {
  CheckAllPin();

  //str = "1!5/7!9/";
  Serial.println(str);
  delay(100);
}




