#include <FastLED.h> 
//init 
int k = 0;
int brightness = 0;
String STRRead;
String temp;
int NoLEDs;
int LEDsOn = 0;
CRGB leds[250];
// allows up to 250 LEDs
CRGB colour(0,0,0);

void setup() {
  delay(500);
  Serial.begin(9600);
  Serial.println("power on");
  NoLEDs = GetInt();
  FastLED.addLeds<WS2812B, 6, GRB>(leds, NoLEDs); 
  //ledtype, ledpin, colortype added to array, numberofleds
  FastLED.setBrightness(50); //low brightness
  //flashes all LEDs inputted to show user which are working
  for (int i=0; i<=NoLEDs; i++){
    leds[i] = CRGB(255, 255, 255);
  }
  FastLED.show();
  delay(500);
  for (int i=0; i<=NoLEDs; i++){
    leds[i] = CRGB(0, 0, 0);
  }
  FastLED.show();
}

CRGB Rainbow(int LED) {
  CRGB newcolour (0,0,0);
  //changes primary colour depending on which 3rd of 255 LED is in
  if(LED < 85) {
    newcolour.g = 0;
    newcolour.r = ((float)LED / 85.0) * 255.0;
    newcolour.b = 255 - newcolour.r;
  } 
  else if(LED < 170) {
    newcolour.g = ((float)(LED - 85) / 85.0) * 255.0;
    newcolour.r = 255 - newcolour.g;
    newcolour.b = 0;
  } 
  else if(LED < 256) {
    newcolour.b = ((float)(LED - 170) / 85.0) * 255.0;
    newcolour.g = 255 - newcolour.b;
    newcolour.r = 0;
  }
  return newcolour;
}

CRGB InputColour(){
  //input colour function
  CRGB newcolour(0,0,0);
  newcolour.r = GetInt();
  newcolour.g = GetInt();
  newcolour.b = GetInt();
  return newcolour;
  }

int GetInt(){
  //parsing int
  String outint;
  char inchar;
  char endmarker = '~';
  while (true){
    //loop until no more data is received or until end maker is shown
    while (Serial.available() > 0){
      //while there are still bytes to read
      inchar = Serial.read();
      if (isDigit(inchar) || inchar =='-'){
        outint += inchar;
      }
      else if (inchar == endmarker){
        return outint.toInt();
      }
    }
  } 
}

String GetString(){
  //for parsing strings
  String outstring;
  char inchar;
  char endmarker = '~';
  //strings cant contain '~'
  while (true){
    while (Serial.available() > 0){
      inchar = Serial.read();
      if (inchar == endmarker){
        return outstring;
      }
      else{
        outstring += inchar;
      }
    }
  }
}

void DisplayLEDs(){
  //loop to show LEDs, reads ints constantly 
  //as should be given ints constantly
  while (LEDsOn > -1){
    for(int i = NoLEDs - 1; i >= 0; i--) {
      if (i < LEDsOn){
       if (colour == CRGB(0,0,0)){
         leds[i] = Rainbow((i * 256 / 50 + k) % 256);
       }
       else{
         leds[i] = colour;
       }
      }
      else {
        leds[i] = CRGB(0,0,0);
      }
    }    
    FastLED.show(); 
    k++;
    k = k % 255;
    LEDsOn = GetInt();    
  }  
}

void loop() {
  //main loop
  while (Serial.available()>0){
    STRRead = GetString();
    //inputs decision for this 'menu'
    if (STRRead == "colour"){
      colour = InputColour();
      //shows the user which colour they entered
      for (int i=0; i<=NoLEDs; i++){
        leds[i] = colour;
      }
      FastLED.show();
      delay(500);
      for (int i=0; i<=NoLEDs; i++){
        leds[i] = CRGB(0, 0, 0);
      }
      FastLED.show();
    }
    else if(STRRead == "music"){
      //starts displayLEDs loop
      DisplayLEDs();
      LEDsOn = 0;
      for (int i=0; i<=NoLEDs; i++){
        leds[i] = CRGB(0, 0, 0);
      }
      FastLED.show();
    }
    else if(STRRead == "brightness"){
      brightness = GetInt();
      //shows the user the brightness they entered
      FastLED.setBrightness(brightness);
      FastLED.show();
    }
    }
    
  }
  
