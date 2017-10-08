# VoishoEmbedded2
ボイスロイドの育成LSIゲーム
  

### [マニュアル](https://github.com/wallstudio/VoishoEmbedded/blob/master/Misc/Manual.md)   
![](Misc/img/goods2.jpg "")  

### ハッキングの種明かし

常駐型のレシーバーの方は，デバイスに変更があった際のイベントに引っ掛けて全ドライブをスキャンし， YukaKurageControl.exe を見つけたら実行しているだけです．このイベント，結構安易に発生するので多重起動を Mutex で禁止しています．

非常駐型は Leonard が組み込みでHIDとして振舞えるので Win+R で PowerShell を起動し，A-Z全ドライブ上の YukaKurageControl.exe を実行します．もちろん一つ以外は失敗するのですが，cmdと違って余計なGUIウィンドウが出てこないのでこれでいけます．

ちなみに，VOICEROIDに食わせる台詞は[Docomoの雑談API](https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=dialogue&p_name=api_reference#tag01)をたたいています．Docomoなので堅いBotなのかと思いきや結構卑猥な言葉を吐くお茶目（？）なAPIでした．

### Lisence

copyright (c) 2017 WallStudio   
Released under the GPL license  
https://www.gnu.org/licenses/gpl.html

また以下の公開されたコードを含みます．

[blkcatman/VoiceroidTalker  
copyright (c) 2015 Tatsuro Matsubara](https://github.com/blkcatman/VoiceroidTalker)  
Released under the MIT license  
http://opensource.org/licenses/mit-license.php

[Keyboard_jp.h](http://mgt.blog.so-net.ne.jp/2016-01-14)  
Copyright (c) 2015, Arduino LLC  
Original code (pre-library): Copyright (c) 2011, Peter Barrett Modified for Japanese 106/109 Keyboard by Toshiyuki UENO MMXVI This library is free software; you can redistribute it y it under the terms of the GNU Lesser General Public License as published by the Free Software Foundation; either version 2.1 of the License, or (at your option) any later version.  
This library is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Lesser General Public License for more details.   
You should have received a copy of the GNU Lesser General Public License along with this library; if not, write to the Free Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA    
