   M S B A M L   `   `   `  ���� H  DBlockchainApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BlockchainAppBlockchainApp  "    BlockchainApp.ProductDetails   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:BlockchainApp       � Title$-  'Kapitał trwały | Szczegóły produktu��$	��758��$
��1200��$	��758��$
��1200��.��� -   $��gridProductForm��#��� ��.+��� $D��>Kapitał trwały - szczegółowe informacje dotyczące środka��$��Center=�$
��0,20q�$	��Top=�$
��Bold"�$��18(�� $��Stretch=�$��
10,75,10,0q�$	��Top=� � RowDefinitions �� $
��auto � � ColumnDefinitions �� $��1* ��� $��1* ��� $��1* �.+��� $��Dane podstawowe��$��0��$��0��$��Stretch=�$��
10,10,10,0q�$	��Top=�����  �� Color# � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��20* ��� $	��14* �.+��� $��Nr ewidencyjny��$��0��$��0��$
��Left=�$��0q�$��Center=��� -    ��Name$ tbRegNumber��$��0��$��1��$��Stretch=�$	��5,8q� �� TextWrapping$
 Wrap=�$��Stretch=�{�.  �� $��Nazwa��$��1��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbName��$��1��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��Numer fabryczny��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbSerialNumber��$��2��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��	Producent��$��3��$��0��$
��Left=�$��0q�$��Center=��� -   $ 
tbProducer��$��3��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��Model��$��4��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbModel��$��4��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��5��$��0��$��Symbol KŚT��$
��Left=�$��0q�$��Center=��� -   $ tbSymbolKST��$��5��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��6��$��0��$��Rodzaj środka��$
��Left=�$��0q�$��Center=��� -    ��Name$ tbType��$��6��$��1��$��Stretch=�$	��5,8q�$��Stretch=�{�.  .@��� $��Biurka��$
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
��Daty��$��0��$��1��$��Stretch=�$��
10,10,10,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��20* ��� $	��14* �.+��� $��Data zakupu��$��0��$��0��$
��Left=�$��0q�$��Center=�(  "System.Windows.Controls.DatePicker  -	     Name$ tbPurchaseDate��$��0��$��1��$��Stretch=�$	��5,8q�$��Stretch=�{�.  �� $$��Data rozpoczęcia użytkowania��$��1��$��0��$
��Left=�$��0q�$��Center=�  -
   $ tbStartDate��$��1��$��1��$��Stretch=�$	��5,8q�$��Stretch=�{�.  �� $$��Data zakończenia użytkowania��$��2��$��0��$
��Left=�$��0q�$��Center=�$	��178��$��25��  -   $ 	tbEndDate��$��2��$��1��$��Stretch=�$	��5,8q�$��Stretch=��� $#��Data rozpoczęcia amortyzacji��$��3��$��0��$
��Left=�$��0q�$��Center=�  -   $ tbDepreciationStartDate��$��3��$��1��$��Stretch=�$	��5,8q�$��Stretch=�{�.  �� $��Rok produkcji��$��4��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbProductionYear��$��4��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $��Data końca gwarancji��$��5��$��0��$
��Left=�$��0q�$��Center=�  -   $ tbWarrantyDate��$��Stretch=�$��5��$��1��$	��5,8q�$��Stretch=��� $��Lokalizacja��$��0��$��2��$��Stretch=�$��
10,10,10,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��20* ��� $	��14* �.+��� $��Adres użytkowania��$��0��$��0��$
��Left=�$��0q�$��Center=��� -   $ 	tbAddress��$��0��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $��Dział��$��1��$��0��$
��Left=�$��0q�$��Center=��� -   $ 	tbSection��$��1��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $��Numer pomieszczenia��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbRoomNumber��$��2��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $��Osoba odpowiedzialna��$��3��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbResponsiblePerson��$��3��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $��Amortyzacja��$��0��$��2��$��Stretch=�$��10,210,10,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��20* ��� $	��14* �.+��� $!��Wartość początkowa (zł)��$��0��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbOriginValue��$��0��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��Stawka amortyzacji (%)��$��1��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbDepreciationRate��$��1��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=��� $#��Wartość zamortyzowana (zł)��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbAmortizedValue��$��2��$��1��$��Stretch=�$	��5,8q�$
 Wrap=�$��Stretch=�{�.  �� $��Status��$��3��$��0��$
��Left=�$��0q�$��Center=��� -   $ cbStatus��$��3��$��1��$��Stretch=�$	��5,8q�$��Stretch=�.@��� $��Amortyzowany��$��Stretch=��� $��Zamortyzowany��$��Stretch=��� $��Zlikwidowany��$��Stretch=��� $��	Sprzedany��$��Stretch=��� $��
Skradziony��$
��Left=�$��118.4���� $��Zepsuty��$
��Left=�$��118.4���� $��Dodatkowe informacje��$��0��$��2��$��Stretch=�$��10,330,10,0q�$	��Top=����� # � ��.���  �� $��55 � �� $	��20* ��� $	��52* �.+��� $��Uwagi��$��0��$��0��$
��Left=�$��0q�$��Center=��� -    ��Name$ tbDescription��$��0��$��1��$��Stretch=�$	��5,8q�$��Stretch=�. �/� .T�J� .*�.*��� ��� *
Data.Uwagi+�� -   	 ��Name$	 lbPermissionsWarning��$h��bUżytkownik, który nie utworzył danego zasobu nie ma możliwości wprowadzania do niego zmian!!!��$��Right=�$��0,500,210,0q�$	��Top=����	#FFFA0D0D$
��Bold"�$y�Hidden=��� -   
 ��Name$
 btnConfirmChanges��$��Wprowadź zmiany��$��Right=�$��
0,495,20,0q�$	��Top=�$	��178��$��35��$
��Bold"��� -    ��Name$ lstProductLink��$
��Left=�$��20,570,20,20q�$��Stretch=�$��2�� �� View ��  �� AllowsColumnReorder . P��� $��Lp�� �� Width$
 auto�� �� DisplayMemberBinding �� *Index+�� $��
Nr Inwent.��$
 auto�� �� *Data.NumerInwentaryzacyjny+�� $��Nazwa środka��$
 auto�� �� *
Data.Nazwa+�� $��Numer fabryczny��$
 auto�� �� *Data.NumerFabryczny+�� $��	Producent��$
 auto�� �� CellTemplate �� .R���  �� TextAlignment$
 Left=�$��12(�
����  �$
��Bold"�$��Italic$����� *Data.Producent+�� $��Model��$
 auto�� �� *
Data.Model+�� $��Symbol KŚT��$
 auto�� �� *Data.SymbolKST+�� $��Rodzaj środka��$
 auto�� �� *Data.Rodzaj+�� $��Data zakupu��$
 auto�� �� *Data.DataZakupu+�� $��Data przyjecia��$
 auto�� �� *Data.DataPrzyjecia+�� $��Data zbycia��$
 auto�� �� *Data.DataZbycia+�� $��Data rozp. amort.��$
 auto�� �� *!Data.DataRozpoczeciaAmortyzacji+�� $��Rok produkcji��$
 auto�� �� *Data.RokProdukcji+�� $��Data końca gwarancji��$
 auto�� �� *Data.DataKoncaGwarancji+�� $��Miejsce użytkowania��$
 auto�� �� *Data.MiejsceUzytkowania+�� $��Dział��$
 auto�� �� *
Data.Dzial+�� $��Numer pomieszczenia��$
 auto�� �� *Data.NumerPomieszczenia+�� $��Osoba odpowiedzialna��$
 auto�� �� *Data.OsobaOdpowiedzialna+�� $��Wartość poczatkowa��$
 auto�� �� *Data.WartoscPoczatkowa+�� $��Stawka amortyzacji��$
 auto�� �� *Data.StawkaAmortyzacji+�� $��Wartość zamortyzowana��$
 auto�� �� *Data.WartoscZamortyzowana+�� $��Status��$
 auto�� �� *Data.Status+�� $��ID modyfikatora��$
 auto�� �� * Data.IdentyfikatorModyfikatora+�� $��Data modyfikacji��$
 auto�� �� *	TimeStamp+�� $��Stretch=�$��4��$��20,552,20,0q�$	��Top=�#��� ���� $$�� Historia wprowadzonych zmian ��$��Center=�$��	0,537,0,0q�$	��Top=�$
��Bold"�$��14(�#��� ��