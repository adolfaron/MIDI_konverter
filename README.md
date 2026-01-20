# MIDI_Konverter

A **MIDI_Konverter** egy Windows-os alkalmazás, amely lehetővé teszi MIDI jelek és soros port (pl. Arduino) adatainak kezelését és összekapcsolását. Segítségével a MIDI kontrollerek jelei hatással lehetnek a számítógép hangkimeneteire.

---

## Fő funkciók

- **MIDI be- és kimenet kezelése**
  - Támogatja az összes csatlakoztatott MIDI eszközt.
  - MIDI Control Change (CC), Note On/Off események feldolgozása.
- **Soros port interfész**
  - Arduino vagy más soros eszközök adatainak olvasása.
  - Soros port adatainak konvertálása MIDI üzenetekre.
- **Hangkimenet vezérlés**
  - A MIDI CC jelek alapján a számítógép hangkimenetének (Master Volume) szabályozása.
- **Valós idejű naplózás**
  - MIDI és soros port események megjelenítése log ablakban.
- **Beállítások mentése és betöltése**
  - MIDI és COM portok, vezérlőleképezések, és egyéb paraméterek tárolása JSON fájlban.
- **Eszközváltozás figyelése**
  - Új MIDI vagy audio eszköz csatlakoztatásakor automatikus frissítés.

---

## Telepítés

1. **Futtatható állomány létrehozása** (ha a forráskódot használod):  
   ```bash
   dotnet build
   ```

2. Győződj meg róla, hogy a **NAudio** NuGet csomag telepítve van.

3. Csatlakoztasd a kívánt MIDI eszközöket és/vagy soros eszközöket.

---

## Használat

1. Indítsd el az alkalmazást.  
2. Válaszd ki a MIDI bemenetet (`midiBeVal`) és kimenetet (`midiKiVal`).  
3. Válaszd ki a COM portot (`ComVal`), ha soros eszközt használsz.  
4. Válaszd ki a hangkimenetet (`kimenetValaszt`) és rendeld hozzá a kívánt MIDI CC számot (`numericCC`) a vezérléshez.  
5. Kattints a **Start** gombra a híd elindításához.  
6. A napló (`textBoxLog`) valós időben mutatja a MIDI és soros eseményeket.

---

## Beállítások

A beállítások a következőket tartalmazzák:

- MIDI bemenet és kimenet index  
- COM port index és soros paraméterek (baud rate, parity, stb.)  
- Kimeneti eszköz → CC leképezés  
- Alkalmazás állapot (bekapcsolva vagy kikapcsolva)  

A beállítások a felhasználó AppData mappájában kerülnek tárolásra:

```
%APPDATA%\MIDI_Konverter\settings.json
```

---

## Fejlesztéshez szükséges könyvtárak

- [.NET Windows Forms](https://learn.microsoft.com/dotnet/desktop/winforms/)  
- [NAudio](https://github.com/naudio/NAudio) (MIDI és audio kezelése)

---

## Kód felépítése

- `Form1.cs` – fő felület és logika: MIDI és soros port kezelése, hangkimenet vezérlése.  
- `BeallitasKezelo.cs` – beállítások betöltése és mentése JSON fájlba.  
- `tick` timer – valós idejű CC vezérlés a hangkimenetekhez.  
- `MidiIn_uzenetJott` – MIDI események fogadása és logolása.  
- `SerialPort_uzenetJott` – soros port események feldolgozása, MIDI üzenetek küldése.

---

## Megjegyzések

- A program Windows rendszeren működik.  
- Az Arduino vagy más soros eszköz adatainak megfelelően kell beállítani a soros port paramétereket.  
- A CC vezérlések a számítógép hangkimenetére is hatással lehetnek (Master Volume).

---

## Licenc

Ez a projekt nyílt forráskódú, saját felelősségre használható.
