//............................................................
// Copyright (C) 2017 WallStudio
// Contact  Website: https://wallstudio.github.io/
//          Twitter: https://twitter.com/yukawallstudio
//............................................................

#include "Keyboard_jp.h"

void StartProcess(char* name){
  delay(1000);
  Keyboard.begin();
  Keyboard.press(KEY_LEFT_GUI);
  Keyboard.press('r');
  Keyboard.releaseAll();
  delay(300);
  Keyboard.print(name);
  delay(100);
  Keyboard.press(KEY_RETURN);
  Keyboard.releaseAll();
}
void Command(char* command){
  Keyboard.print(command);
  Keyboard.press(KEY_RETURN);
  Keyboard.releaseAll();
}

void setup(){
  delay(3000);
  StartProcess("powershell");
  delay(5000);
  /*
  Command("Invoke-WebRequest -Uri https://pbs.twimg.com/media/DKfP3dFU8AAEfZ2.png -OutFile $env:USERPROFILE\\Desktop\\yukamaki.png");
  Command("start \"$env:USERPROFILE\\Desktop\\yukamaki.png\"");
  */
  int commandSize = 30*30;
  char command[commandSize];
  int j;
  // スペースで初期化
  for(j=0; j<commandSize; j++){
    command[j] = ' ';
  }
  command[commandSize-1] = '\0';
  // 各ドライブレターをコンカチ
  char i;
  for(i='A'; i<='Z'; i++){
    int commandCursor = (i-'A') * 30;
    // レターをセット
    command[commandCursor] = i;
    // ファイル名セット 
    char file[] = ":\\YukaKurageControl.exe; ";
    int k;
    for(k=0; k<sizeof(file)/sizeof(char)-1; k++){
      command[commandCursor+k+1] = file[k];
    }
  }
  command[commandSize-10] = 'e';
  command[commandSize-9] = 'x';
  command[commandSize-8] = 'i';
  command[commandSize-7] = 't';
  // シリアル通信でコマンド内容を送信
  Serial.begin(9600);
  Serial.println(command);
  // コマンド発行
  Command(command);
}

void loop(){
}
