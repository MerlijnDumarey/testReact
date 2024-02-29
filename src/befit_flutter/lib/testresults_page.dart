import 'package:flutter/material.dart';
import 'student_app_bar.dart';

class TestResultsPage extends StatefulWidget {
  final String selectedTest;

  const TestResultsPage({super.key, required this.selectedTest});

  @override
  // ignore: library_private_types_in_public_api
  _TestResultsPageState createState() => _TestResultsPageState();
}

class _TestResultsPageState extends State<TestResultsPage> {
  String name = '';
  String email = '';

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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: StudentAppBar(name: name, email: email),
      body: SingleChildScrollView(
        child: Container(
          padding: const EdgeInsets.all(16),
          child: Center(
            child: SizedBox(
              width: 800,
              child: Table(
                children: [
                  // Header row with 5 columns
                  const TableRow(
                    children: [
                      TableCell(child: Center(child: Text(''))),
                      TableCell(child: Center(child: Text('Sem 1'))),
                      TableCell(child: Center(child: Text('Sem 2'))),
                      TableCell(child: Center(child: Text('Sem 3'))),
                      TableCell(child: Center(child: Text('Sem 4'))),
                    ],
                  ),
                  // Empty row for spacing
                  const TableRow(
                    children: [
                      TableCell(child: SizedBox(height: 8)),
                      TableCell(child: SizedBox(height: 8)),
                      TableCell(child: SizedBox(height: 8)),
                      TableCell(child: SizedBox(height: 8)),
                      TableCell(child: SizedBox(height: 8)),
                    ],
                  ),
                  // Content rows
                  TableRow(
                    children: [
                      // First row content
                      TableCell(
                          child: Center(child: Text(widget.selectedTest))),
                      const TableCell(child: Center(child: Text('xxx'))),
                      const TableCell(child: Center(child: Text('xxx'))),
                      const TableCell(child: Center(child: Text('xxx'))),
                      const TableCell(child: Center(child: Text('xxx'))),
                    ],
                  ),
                  const TableRow(
                    children: [
                      // Second row content
                      TableCell(child: Center(child: Text('Klasgemiddelde'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                    ],
                  ),
                  const TableRow(
                    children: [
                      // Third row content
                      TableCell(child: Center(child: Text('Eurofit norm'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                      TableCell(child: Center(child: Text('xxx'))),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
