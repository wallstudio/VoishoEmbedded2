//............................................................
// Copyright (C) 2017 WallStudio
// Contact  Website: https://wallstudio.github.io/
//          Twitter: https://twitter.com/yukawallstudio
//............................................................
#include <LCD5110_Graph.h>
#include <SoftwareSerial.h>
#include <DFPlayer_Mini_Mp3.h>
#include <avr/pgmspace.h>
#include "bitmap.h"
#include "Game2D.h"
#include "QREncode.h"
//............................................................
//............................................................
//..................... HARDWARE PREPARE .....................
//............................................................
//............................................................
// LCD .......................................................
#define SCREEN_BUF_SYSE 504
#define SCREEN_WIDTH    84
#define SCREEN_HEIGHT   48
extern uint8_t SmallFont[];
GameLCD screen(SCREEN_WIDTH, SCREEN_HEIGHT, SCREEN_BUF_SYSE, 8, 9, 10, 11, 12);
//............................................................
// Audio .....................................................
//............................................................
#define VOLUME 30
#define SE_BTN_OK 1
#define SE_BTN_NG 2
#define SE_GYN_0  3
//............................................................
// I/O .......................................................
//............................................................
#define I0 14
#define I1 15
#define I2 16
#define O0 2
//............................................................
// GameDefine ................................................
//............................................................
#define MENU_SEL_CNT 7
int GameFps = 60;
bool BufL, BufC, BufR;
long Frame = 0;
// Status
int Life = 6;
int Love = 0;
int Hungery = 0;
int Sick = 0;
int Dirty = 0;
// Setting
int Volume = VOLUME;
int Luminance = 0;
// Input
bool btnL,btnC, btnR, btnDownL, btnDownC, btnDownR ,btnUpL, btnUpC, btnUpR;
void BtnDetectAll(){
  btnL = ButtonDetect(I0, &BufL, Status);
  btnC = ButtonDetect(I1, &BufC, Status);
  btnR = ButtonDetect(I2, &BufR, Status);
  btnDownL = ButtonDetect(I0, &BufL, DonwTrig);
  btnDownC = ButtonDetect(I1, &BufC, DonwTrig);
  btnDownR = ButtonDetect(I2, &BufR, DonwTrig);
  btnUpL = ButtonDetect(I0, &BufL, UpTrig);
  btnUpC = ButtonDetect(I1, &BufC, UpTrig);
  btnUpR = ButtonDetect(I2, &BufR, UpTrig);
  BufL = btnL;
  BufC = btnC;
  BufR = btnR;
}
void SceneInit(){
  BtnDetectAll();
  Frame++;
  screen.Clear(0x00);
}
//............................................................
//............................................................
// Initialize ................................................
//............................................................
void InitAudio(){
  mp3_set_serial(Serial);
  delay(1);
  mp3_set_volume(VOLUME);
}
void InitIO(){
  Serial.begin(9600);
  pinMode(O0,OUTPUT);
  pinMode(I0,INPUT);
  pinMode(I1,INPUT);
  pinMode(I2,INPUT);
  digitalWrite(O0, HIGH);
}
void setup(){
  InitIO();
  screen.ClearInitLCD();
  screen.setFont(SmallFont);
  InitAudio();
  Start();
}
void loop(){
  screen.Clear(0x00);
  Update();
  screen.update();
  delay(1000/GameFps);
}
bool Minigame0(int countDown);
//............................................................
//............................................................
//......................... GAME MAIN ........................
//............................................................
//............................................................
// GameObjects ...............................................
//............................................................
// Interface
GameObject *LifeSign;
GameObject *LoveSign;
GameObject *HungrySign;
GameObject *SichSign;
GameObject *DirtySign;
// CHaractor
GameObject *Maki;
GameObject *Hand;
GameObject *Hurt;
// Menu
GameObject *selectionIcons;
GameObject *selectionsText;
//............................................................
//............................................................
void Start(){
  // Interface
  uint8_t *lifeTex[] = {Bmp_plus0, Bmp_plus1, Bmp_plus2, Bmp_plus3, Bmp_plus4, Bmp_plus5, Bmp_plus6};
  LifeSign = new GameObject(&screen, lifeTex, 7, 6, 2, 2);
  uint8_t *loveTex[] = {Bmp_hurt0, Bmp_hurt1, Bmp_hurt2, Bmp_hurt3, Bmp_hurt4, Bmp_hurt5, Bmp_hurt6};
  LoveSign = new GameObject(&screen, loveTex, 7, 3, 59, 2);
  uint8_t *hungryTex[] = {Bmp_meal};
  HungrySign = new GameObject(&screen, hungryTex, 1, 0, 7, 11);
  uint8_t *sickTex[] = {Bmp_poison};
  SichSign = new GameObject(&screen, sickTex, 1, 0, 58, 11);
  uint8_t *dirtyTex[] = {Bmp_dirt0, Bmp_dirt1, Bmp_dirt2};
  DirtySign = new GameObject(&screen, dirtyTex, 1, 0, 5, 30);
  // Charactor
  uint8_t *makiTex[] = {Bmp_gyun0, Bmp_gyun1};
  Maki = new GameObject(&screen, makiTex, 2);
  uint8_t *handTex[] = {Bmp_hand};
  Hand = new GameObject(&screen, handTex, 1);
  uint8_t *hurtTex[] = {Bmp_hurt};
  Hurt = new GameObject(&screen, hurtTex, 1);
  //Menu
  uint8_t *selectionsIconTex[] = {
    Bmp_menu_stroke,
    Bmp_menu_lasagna, 
    Bmp_menu_clean, 
    Bmp_menu_game, 
    Bmp_menu_garally, 
    Bmp_menu_setting, 
    Bmp_menu_return
  };
  selectionIcons = new GameObject(&screen, selectionsIconTex, MENU_SEL_CNT, 0, 6, 10);
  uint8_t *selectionsTextTex[] = {
    Bmp_txt_naderu,
    Bmp_txt_gohan,
    Bmp_txt_souji,
    Bmp_txt_asobu,
    Bmp_txt_gyarari,
    Bmp_txt_settei,
    Bmp_txt_modoru
  };
  selectionsText = new GameObject(&screen, selectionsTextTex, MENU_SEL_CNT, 0, 6, 30);
}
//............................................................
//............................................................
void Update(){
  SceneInit();
  // Interface
  LifeSign->Rend();
  LoveSign->Rend();
  screen.print(" -- ", 2, 40);
  screen.print("MENU", 30, 40);
  screen.print(" -- ", 58, 40);
  HungrySign->Rend();
  SichSign->Rend();
  DirtySign->Rend();
  // Maki
  Maki->Ty = 10;
  Maki->Tx = (int)(sin((float)Frame/8)*2+screen.Width/2-Maki->GetWidth()/2);
  Maki->TexNo = Frame/16%2;
  Maki->Rend();
  // Input
  if(btnDownL){
    mp3_play(SE_BTN_NG);
  }
  if(btnDownC){
    mp3_play(SE_BTN_OK);
    MenuLauncher();
  }
  if(btnDownR){
    mp3_play(SE_BTN_NG);
    //Mingame0Launcher();
  }
}
//............................................................
//............................................................
//......................... MENU SCENE .......................
//............................................................
//............................................................
//............................................................
bool Menu(int *timer){
  SceneInit();
  // Interface
  LifeSign->Rend();
  screen.print("MENU", 30, 1);
  LoveSign->Rend();
  screen.print(" <  ", 2, 40);
  screen.print(" OK ", 30, 40);
  screen.print("  > ", 58, 40);
  // Selection
  selectionIcons->Rend();
  selectionsText->Rend();
  screen.printNumI(selectionIcons->TexQuant, 44, 20);
  screen.print("/", 39, 20);
  screen.printNumI(selectionIcons->TexNo+1, 34, 20);
  // Maki
  Maki->Ty = 10;
  Maki->Tx = (int)(sin((float)Frame/8)*2+screen.Width/2-Maki->GetWidth()/2)+24;
  Maki->TexNo = Frame/16%2;
  Maki->Rend();
  // Input
  if(btnDownL){
    mp3_play(SE_BTN_OK);
    *timer = 0;
    selectionIcons->TexNo += selectionIcons->TexQuant - 1;
    selectionsText->TexNo += selectionIcons->TexQuant - 1;
    selectionIcons->TexNo %= selectionIcons->TexQuant;
    selectionsText->TexNo %= selectionIcons->TexQuant;
  }
  if(btnDownC){
    mp3_play(SE_BTN_OK);
    switch(selectionIcons->TexNo){
      case 0: // Touch
        break;
      case 1: // Feed
        break;
      case 2: // Clean
        break;
      case 3: // Game
        break;
      case 4: // Garally
        break;
      case 5: // Config
        break;
      case 6: // Return
        return false;
        break;
      default: break;
    }
  }
  if(btnDownR){
    mp3_play(SE_BTN_OK);
    *timer = 0;
    selectionIcons->TexNo++;
    selectionsText->TexNo++;
    selectionIcons->TexNo %= selectionIcons->TexQuant;
    selectionsText->TexNo %= selectionIcons->TexQuant;
  }
  return true;
}
void MenuLauncher(){
  int menuTimeOut = 100;
  for(int i=0; i<menuTimeOut; i++){
    if(!Menu(&i)) break;
    screen.update();
  }
}

//............................................................
//............................................................
//......................... SUB SCENE ........................
//............................................................
//............................................................
//............................................................
void Mingame0Launcher(){
    //Manual
    screen.Clear(0x00);
    screen.print("MINIGAME START", 0, 10);
    screen.print("POSITION KMAKI", 0, 23);
    screen.print("PLZ PUSH BUTON", 0, 31);
    screen.update();
    delay(10000);
    //Main
    int loveCnt = 0;
    for(int i=300; i>=0; i--) Mingame0(i, &loveCnt);
    //Result
    screen.Clear(0x00);
    screen.print("YOUR SCORE!", 0, 10);
    screen.print(" LOVE: ", 0, 20);
    screen.printNumI(loveCnt, 40, 20);
    screen.update();
    delay(10000);
    //Future QR
    screen.Clear(0x00);
    screen.print("FUTURE", 0, 2);
    screen.print("  ->  ", 0, 10);
    screen.print("PLZ COME", 0, 32);
    screen.print("KOETSUKI", 0, 40);
    uint8_t infoData[] = "goo.gl/eWF1m2";
    uint8_t qrTex[21*3*2];
    GameObject* qr = new GameObject(&screen, QREncode(&screen, infoData, sizeof(infoData), qrTex), 21, 21);
    qr->Scl = 1;
    qr->Tx = 55;
    qr->Ty = 10;
    qr->Rend();
    screen.update();
    delay(30000);
    delete qr;
}
void Mingame0(int countDown, int* loveCnt){
  SceneInit();
  //Information
  screen.print("TIME", 1, 1);
  screen.printNumI(countDown, 50, 1);
  screen.print("SCORE", 1, 40);
  screen.printNumI(*loveCnt, 50, 40);
  //Interface
  //Maki
    // <-----------84-------------->
    // <--------59---------><--27-->
    // <-20--><--20-><--19->
  Maki->Ty = 10;
  Maki->Tx = (int)(sin((float)(300-countDown+(random(countDown/40)*3))/8)*16+screen.Width/2-Maki->GetWidth()/2);
  Maki->TexNo = Frame/16%2;
  Maki->Rend();
  Hurt->Tx = Maki->Tx -5;
  Hurt->Ty = Maki->Ty;
  //Hand
  if(btnL){
    Hand->Tx = 10;
    Hand->Ty = 13 + (int)(sin((float)Frame/2)*2);
    Hand->Rend();
    if(Maki->Tx<20) Hurt->Rend();
  }else if(btnC){
    Hand->Tx = 30;
    Hand->Ty = 13 + (int)(sin((float)Frame/2)*2);
    Hand->Rend();
    if(Maki->Tx>=20 && Maki->Tx<40) Hurt->Rend();
  }else if(btnR){
    Hand->Tx = 50;
    Hand->Ty = 13 + (int)(sin((float)Frame/2)*2);
    Hand->Rend();
    if(Maki->Tx>=40) Hurt->Rend();
  }
  //HitCheck
  if((btnDownL && Maki->Tx<20) || (btnDownC && Maki->Tx>=20 && Maki->Tx<40) || (btnDownR && Maki->Tx>=40)) {
      (*loveCnt)++;
      mp3_play(1);
  } else if(btnDownL || btnDownC || btnDownR){
      // fail effect
  }
  screen.update();
}
//............................................................
//............................................................
//.......................... END .............................
//............................................................