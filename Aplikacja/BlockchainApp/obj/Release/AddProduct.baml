   M S B A M L   `   `   `  ���� H  DBlockchainApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:BlockchainAppBlockchainApp      BlockchainApp.AddProduct   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -localclr-namespace:BlockchainApp     -     � 
ResizeMode$  NoResize=� � Title$0 *BC - Kapitał trwały | Dodawanie produktu��$	��570��$
��1200��.��� #��� ��.+��� $1��+Kapitał trwały - dodawanie nowego środka��$��Center=�$
��0,20q�$	��Top=�$
��Bold"�$��18(��� $��Dane podstawowe��$
��Left=�$��	35,75,0,0q�$	��Top=�����  �� Color# � ��.���  � RowDefinitions �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 � � ColumnDefinitions �� $	��200 ��� $	��140 �.+��� $��0��$��0��$
��Left=�$��0q�$��Center=�.����  �� Orientation$ 
Horizontal=�.+��� $��Nr ewidencyjny���� 
���  ��$�� *���� -    ��Name$ tbRegNumber��$��0��$��1��$��Center=�$��22��$��0q� �� TextWrapping$
 Wrap=�$��Center=�$	��120���� $��1��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Nazwa���� 
���  ��$�� *���� -   $ tbName��$��1��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Numer fabryczny��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbSerialNumber��$��2��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��	Producent��$��3��$��0��$
��Left=�$��0q�$��Center=��� -   $ 
tbProducer��$��3��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Model��$��4��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbModel��$��4��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��5��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Symbol KŚT���� 
���  ��$�� *���� -   $ tbSymbolKST��$��5��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��6��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Rodzaj środka���� 
���  ��$�� *���� -    ��Name$ tbType��$��6��$��1��$��Center=�$��0q�$��Center=�$	��120��$��22��.@��� $��Biurka��$
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
��Daty��$��Center=�$��0,75,0,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��200 ��� $	��140 �.+��� $��0��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Data zakupu���� 
���  ��$�� *��(  "System.Windows.Controls.DatePicker  -	   	  Name$	 tbPurchaseDate��$��0��$��1��$��Center=�$��0q�$��Center=�$	��120���� $��1��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $$��Data rozpoczęcia użytkowania���� 
���  ��$�� *��  -
   $	 tbStartDate��$��1��$��1��$��Center=�$��0q�$��Center=�$	��120���� $$��Data zakończenia użytkowania��$��2��$��0��$
��Left=�$��0q�$��Center=�$	��178��$��25��  -   $	 	tbEndDate��$��2��$��1��$��Center=�$��0q�$��Center=�$	��120���� $��3��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $#��Data rozpoczęcia amortyzacji���� 
���  ��$�� *��  -   $	 tbDepreciationStartDate��$��3��$��1��$��Center=�$��0q�$��Center=�$	��120���� $��Rok produkcji��$��4��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbProductionYear��$��4��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Data końca gwarancji��$��5��$��0��$
��Left=�$��0q�$��Center=�  -   $	 tbWarrantyDate��$��Center=�$��5��$��1��$��0q�$��Center=�$	��120���� $��Lokalizacja��$��Right=�$��	0,75,35,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��200 ��� $	��140 �.+��� $��Adres użytkowania��$��0��$��0��$
��Left=�$��0q�$��Center=��� -   $ 	tbAddress��$��0��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Dział��$��1��$��0��$
��Left=�$��0q�$��Center=��� -   $ 	tbSection��$��1��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Numer pomieszczenia��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbRoomNumber��$��2��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Osoba odpowiedzialna��$��3��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbResponsiblePerson��$��3��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��Amortyzacja��$��Right=�$��
0,275,35,0q�$	��Top=����� # � ��.���  �� $��40 ��� $��40 ��� $��40 ��� $��40 � �� $	��200 ��� $	��140 �.+��� $��0��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $!��Wartość początkowa (zł)���� 
���  ��$�� *���� -   $ tbOriginValue��$��0��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��1��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Stawka amortyzacji (%)���� 
���  ��$�� *���� -   $ tbDepreciationRate��$��1��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $#��Wartość zamortyzowana (zł)��$��2��$��0��$
��Left=�$��0q�$��Center=��� -   $ tbAmortizedValue��$��2��$��1��$��Center=�$��22��$��0q�$
 Wrap=�$��Center=�$	��120���� $��3��$��0��$
��Left=�$��0q�$��Center=�.���� $ 
Horizontal=�.+��� $��Status���� 
���  ��$�� *���� -   $ cbStatus��$��4��$��1��$��Center=�$��0q�$��Center=�$	��120��.@��� $��Amortyzowany��$
��Left=�$��118.4���� $��Zamortyzowany��$
��Left=�$��118.4���� $��Zlikwidowany��$
��Left=�$��118.4���� $��	Sprzedany��$
��Left=�$��118.4���� $��
Skradziony��$
��Left=�$��118.4���� $��Zepsuty��$
��Left=�$��118.4���� $��Dodatkowe informacje��$
��Left=�$��
35,395,0,0q�$	��Top=����� # � ��.���  �� $��55 � �� $	��200 ��� $	��523 �.+��� $��Uwagi��$��0��$��0��$
��Left=�$��0q�$��Center=��� -   
 ��Name$
 tbDescription��$��0��$��1��$��Center=�$��45��$��0q�$��Center=�$	��500��. �/� .T�J� .*�.*��� �� $'��!* - pola wymagane do wypełnienia��$
��Left=�$��
35,485,0,0q�$	��Top=�$	��241�����	#FFFF0202$
��Bold"��� -    ��Name$ btnSend��$��Dodaj produkt��$��Right=�$��
0,485,35,0q�$	��Top=�$	��120��$��31��$
��Bold"�