   M S B A M L   `   `   `  ���� H  DBlockchainApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BlockchainAppBlockchainApp      BlockchainApp.Connection   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:BlockchainApp     -     � Title$.  (Kapitał trwały | zarządzanie zasobami��$	��700��$
��1200��$
��1200��$	��650��.��� #��� �� � ColumnDefinitions �� $
��595* ��� �� .+��� $/��)Kapitał trwały - zarządzanie środkami��$��Center=�$
��0,20q�$	��Top=�$
��Bold"�$��18(��� $��Right=�$��	0,10,10,0q�$	��Top=�.����  �� Orientation$ 
Horizontal=�.+��� $��Zalogowany użytkownik: ���� -    ��Name$ 
tbUserName��
����  �$
��Bold"��� -   $ 	btnLogOut��
���  ��$
��Bold"�$��
 [Wyloguj]���� $��Podstawowe dane��$��Stretch=�$��10,110,10,0q�$	��Top=�$��0q�����  �� Color# � ��.���  � RowDefinitions �� $��26 ��� $��26 � �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��Nr ewidencyjny��$��0��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=��� -    ��Name$ tbEvidenceNumber��$��0��$��1��$��Stretch=�$��2,2,10,2q� �� TextWrapping$
 Wrap=�$��Stretch=�$
��auto���� $��Nazwa��$��1��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbName��$��1��$��1��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=��� $��Numer fabryczny��$��0��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbSerialNumber��$��0��$��3��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��	Producent��$��1��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ 
tbProducer��$��1��$��3��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Model��$��0��$��4��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbModel��$��0��$��5��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Symbol KŚT��$��1��$��4��$
��Left=�$��0q�$
��10,0q�$��Center=��� -	   $ tbSymbolKST��$��1��$��5��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Rodzaj środka��$��0��$��6��$
��Left=�$��0q�$
��10,0q�$��Center=��� -
    ��Name$ tbType��$��0��$��7��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��.@��� $��Biurka��$
��Left=�$	��320���� $��Drukarki��$
��Left=�$	��320���� $��Kable Ethernetowe��$
��Left=�$	��320���� $��
Kable HDMI��$
��Left=�$	��320���� $��	Kable USB��$
��Left=�$	��320���� $��
Klawiatury��$
��Left=�$	��320���� $��Komputery stacjonarne��$
��Left=�$	��320���� $��Laptopy��$
��Left=�$	��320���� $��Lodówki��$
��Left=�$	��320���� $��	Monitory ��$
��Left=�$	��320���� $��Myszki��$
��Left=�$	��320���� $��Rutery��$
��Left=�$	��320���� $��	Samochody��$
��Left=�$	��320���� $��Sejfy��$
��Left=�$	��320���� $��Szafki ��$
��Left=�$	��320���� $��Szafy��$
��Left=�$	��320���� $��Telefony��$
��Left=�$	��320���� $ ��Urządzenia wielofunkcyjne��$
��Left=�$	��320���� $
��Inne��$
��Left=�$	��320���� $
��Daty��$��Stretch=�$��10,190,10,0q�$	��Top=�$��0q����� # � ��.���  �� $��26 ��� $��26 � �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��Data zakupu��$��0��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=�(  "System.Windows.Controls.DatePicker  -   	  Name$	 tbAcceptanceDate��$��0��$��1��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��$��11(��� $��Rozpocz. użytkowania��$��1��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=�  -   $	 tbStartDate��$��1��$��1��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��$��11(��� $��Zakończ. użytkowania��$��0��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=�  -   $	 	tbEndDate��$��0��$��3��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��$��11(��� $��Rozpocz. amortyzacji��$��1��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=�  -   $	 tbDepreciationStartDate��$��1��$��3��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��$��11(��� $��Rok produkcji��$��0��$��4��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbProductionYear��$��0��$��5��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Data końca gwarancji��$��1��$��4��$
��Left=�$��0q�$
��10,0q�$��Center=�  -   $	 tbWarrantyDate��$��1��$��5��$��Stretch=�$��2,2,10,2q�$��Stretch=�$
��auto��$��11(��� $��Data ost. zmiany��$��0��$��6��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbModifyDate��$��0��$��7��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto��� $��Stretch=�$��10,270,10,0q�$	��Top=� �� $��* � �� $��1* ��� $��1* �.+��� $��0��$��0��$��Lokalizacja��$��Stretch=�$��0q�$	��Top=�$��0q����� # � ��.���  �� $��26 ��� $��26 � �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��Adres użytkowania��$��0��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ 	tbAddress��$��0��$��1��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Dział��$��1��$��0��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ 	tbSection��$��1��$��1��$��Stretch=�$��2,2,10,2q�$
 Wrap=�$��Stretch=��� $��Numer pomieszczenia��$��0��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbRoomNumber��$��0��$��3��$��Stretch=�$��10,2,2,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Osoba odpowiedzialna��$��1��$��2��$
��Left=�$��0q�$
��10,0q�$��Center=��� -   $ tbResponsiblePerson��$��1��$��3��$��Stretch=�$��10,2,2,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��0��$��1��$��Amortyzacja��$��Stretch=�$��0q�$	��Top=�$��0q����� # � ��.���  �� $��26 ��� $��26 � �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��Wartość początkowa od��$��0��$��0��$
��Left=�$��0q�$	��5,0q�$��Center=��� -   $ tbOriginValueFrom��$��0��$��1��$��Stretch=�$��0,2,12,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Wartość początkowa do��$��1��$��0��$
��Left=�$��0q�$	��5,0q�$��Center=��� -   $ tbOriginValueTo��$��1��$��1��$��Stretch=�$��0,2,12,2q�$
 Wrap=�$��Stretch=�$
��auto���� $��Stawka amortyzacji��$��0��$��2��$
��Left=�$��0q�$	��5,0q�$��Center=��� -   $ tbDepreciationRate��$��0��$��3��$��Stretch=�$��0,2,12,2q�$
 Wrap=�$��Stretch=��� $��Stan amortyzacji��$��1��$��2��$
��Left=�$��0q�$	��5,0q�$��Center=��� -   $ cbStatus��$��1��$��3��$��Stretch=�$��0,2,12,2q�$��Stretch=�$
��auto��.@��� $��Amortyzowany��$��Stretch=�$
��auto���� $��Zamortyzowany��$��Stretch=�$
��auto���� $��Zlikwidowany��$��Stretch=�$
��auto���� $��	Sprzedany��$��Stretch=�$
��auto���� $��
Skradziony��$
��Left=�$��118.4���� $��Zepsuty��$
��Left=�$��118.4���� -   
 ��Name$
 lstProducts��$��Stretch=�$��10,425,10,20q�$��Stretch=� �� View ��  �� AllowsColumnReorder . P��� $��Numer�� �� Width$
 auto�� �� DisplayMemberBinding �� *Index+�� $��
Nr Inwent.��$
 auto�� �� *Data.NumerInwentaryzacyjny+�� $��Nazwa środka��$
 auto�� �� CellTemplate �� .R���  �� TextAlignment$
 Left=�$��12(�
����  �$
��Bold"�$��Italic$����� *
Data.Nazwa+�� $��Numer fabryczny��$
 auto�� �� *Data.NumerFabryczny+�� $��	Producent��$
 auto�� �� *Data.Producent+�� $��Model środka��$
 auto�� �� *
Data.Model+�� $��Symbol KŚT��$
 auto�� �� *Data.SymbolKST+�� $��Rodzaj środka��$
 auto�� �� *Data.Rodzaj+�� $��Data zakupu��$
 auto�� �� *Data.DataZakupu+�� $��Data przyjecia��$
 auto�� �� *Data.DataPrzyjecia+�� $��Data zbycia��$
 auto�� �� *Data.DataZbycia+�� $��Data rozp. amort.��$
 auto�� �� *!Data.DataRozpoczeciaAmortyzacji+�� $��Rok produkcji��$
 auto�� �� *Data.RokProdukcji+�� $��Data końca gwarancji��$
 auto�� �� *Data.DataKoncaGwarancji+�� $��Miejsce użytkowania��$
 auto�� �� *Data.MiejsceUzytkowania+�� $��Dział��$
 auto�� �� *
Data.Dzial+�� $��Numer pomieszczenia��$
 auto�� �� *Data.NumerPomieszczenia+�� $��Osoba odpowiedzialna��$
 auto�� �� *Data.OsobaOdpowiedzialna+�� $��Wartość poczatkowa��$
 auto�� �� *Data.WartoscPoczatkowa+�� $��Stawka amortyzacji��$
 auto�� �� *Data.StawkaAmortyzacji+�� $��Wartość zamortyzowana��$
 auto�� �� *Data.WartoscZamortyzowana+�� $��Status��$
 auto�� �� *Data.Status+�� $��ID modyfikatora��$
 auto�� �� * Data.IdentyfikatorModyfikatora+�� $��Data modyfikacji��$
 auto�� �� *	TimeStamp+�� -    ��Name$ btnAdd��$��Dodaj produkt��$
��Left=�$��
10,380,0,0q�$	��Top=�$	��125��$��35��$
��Bold"��� -   $ 
btnDetails��$��Szczegóły produktu��$
��Left=�$��140,380,0,0q�$	��Top=�$	��125��$��35��$
��Bold"��� -   $ btnExpanderOn��$��Pokaż historię��$
��Left=�$��270,380,0,0q�$	��Top=�$	��125��$��35��$
��Bold"��� -   $ btnExpanderOff��$��Ukryj historię��$
��Left=�$��270,380,0,0q�$	��Top=�$	��125��$��35��$y�Hidden=�$
��Bold"��� -   $ btnExcelExport��$��Center=�$��	0,380,0,0q�$	��Top=�$	��130��$��35��.���� $ 
Horizontal=�.+��� $
��Left=�$��/Images/excel.png��$
��Fill=�$��26��$��26��$��2q��� $
��Left=�$��Center=�$��12(�$��2,0,0,0q�$
��Bold"�Eksportuj dane�� -    $ btnFiltr��$��Zastosuj filtry��$��Right=�$��0,380,140,0q�$	��Top=�$	��125��$��35��$
��Bold"��� -!   $ btnClear��$��Wyczyść filtrowanie��$��Right=�$��
0,380,10,0q�$	��Top=�$	��125��$��35��$
��Bold"��� $��Stretch=�$��4��$��
10,95,10,0q�$	��Top=�#��� ���� $�� Filtrowanie danych ��$��Center=�$��0,83,0,0q�$	��Top=�$
��Bold"�$��14(�#��� ���� $��Stretch=�$��4��$��10,360,10,0q�$	��Top=�#��� ��