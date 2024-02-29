import 'package:flutter/material.dart';
import "student_app_bar.dart";
import 'testresults_page.dart';

class TestSelectorPage extends StatefulWidget {
  const TestSelectorPage({
    super.key,
  });

  @override
  _TestSelectorPageState createState() => _TestSelectorPageState();
}

class _TestSelectorPageState extends State<TestSelectorPage> {
  String name = '';
  String email = '';
  int selectedCard1Index = -1; // Initialize to -1 (no selection for Card 1)
  int selectedCard2Index = -1; // Initialize to -1 (no selection for Card 2)
  Map<String, List<String>> categoryMap = {
    'Kracht': ['Push-ups', 'Sit-ups', 'Squats', 'Plank'],
    'Lenigheid': ['Touch toes', 'Sit and reach', 'Shoulder stretch'],
    'Uithouding': ['Cooper test', '5 km run', '10 km run'],
    'Snelheid': ['40m sprint', '100m sprint', '200m sprint'],
    'Coördinatie': ['Balancing', 'Juggling', 'Jump rope'],
    'Evenwicht': ['One leg stand', 'Balance beam', 'Yoga poses'],
  };

  @override
  void initState() {
    super.initState();
    fetchData();
  } //Initiate the data fetching process when the widget is created

  Future<void> fetchData() async {
    // Simulate fetching data from an API
    await Future.delayed(const Duration(seconds: 1));
    //Simulate delay of fetching data for appbar from an API
    //replace delay with actual API call

    setState(() {
      name = 'John Doe'; // Replace with actual data
      email = 'john@doe.com'; // Replace with actual data
    });
  }

  List<String> itemsCard1 = [
    'Kracht',
    'Lenigheid',
    'Uithouding',
    'Snelheid',
    'Coördinatie',
    'Evenwicht',
    // Add more items as needed
  ];

  List<String> itemsCard2 = [];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: StudentAppBar(name: name, email: email),
      body: Center(
        child: SingleChildScrollView(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Row(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment:
                    CrossAxisAlignment.start, // Align boxes at the start (top)
                children: [
                  _buildCard(itemsCard1, selectedCard1Index, (index) {
                    setState(() {
                      selectedCard1Index = index;
                      itemsCard2 = categoryMap[itemsCard1[index]] ?? [];
                      selectedCard2Index = -1;
                    });
                  }),
                  _buildCard(itemsCard2, selectedCard2Index, (index) {
                    setState(() {
                      selectedCard2Index = index;
                    });
                  }),
                ],
              ),
              const SizedBox(height: 16), //Add spacing between the children
              Row(
                // Align boxes center
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  _buildbutton(),
                ],
              )
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildCard(
      List<String> items, int selectedIndex, Function(int) onTap) {
    return Container(
      width: 180, // Adjust the width as needed
      margin: const EdgeInsets.all(10),
      padding: const EdgeInsets.all(8),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(10),
      ),
      child: Card(
        child: Padding(
            padding: const EdgeInsets.all(8.0),
            child: items.isNotEmpty
                ? ListView.builder(
                    shrinkWrap: true,
                    itemCount: items.length,
                    itemBuilder: (context, index) {
                      final isSelected = index ==
                          selectedIndex; // Replace with actual selection state
                      return ListTile(
                        title: Text(items[index]),
                        tileColor: isSelected
                            ? Colors.blue[300]
                            : Colors.white, // Adjust the color as needed
                        onTap: () {
                          onTap(index);
                        },
                      );
                    },
                  )
                : Container(
                    height: 60, // Set a minimum height when itemsCard2 is empty
                    alignment: Alignment.center,
                    padding: const EdgeInsets.all(8),
                    child: const Text('Selecteer een categorie'),
                  )),
      ),
    );
  }

  Widget _buildbutton() {
    return Container(
      margin: const EdgeInsets.all(10),
      padding: const EdgeInsets.all(8),
      child: ElevatedButton(
        onPressed: () {
          if (selectedCard2Index != -1) {
            String selectedTest = itemsCard2[selectedCard2Index];
            Navigator.push(
              context,
              MaterialPageRoute(
                  builder: (context) =>
                      TestResultsPage(selectedTest: selectedTest)),
            );
          } else {
            // Display snackbar with error message
            ScaffoldMessenger.of(context).showSnackBar(
              const SnackBar(
                content: Center(child: Text('Selecteer een test aub!')),
                duration: Duration(seconds: 3),
              ),
            );
          }
        },
        child: const Text('Bekijk score'),
      ),
    );
  }
}
