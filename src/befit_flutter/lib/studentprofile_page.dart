import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'student_app_bar.dart';
import "main.dart" as main;

class StudentProfilePage extends StatefulWidget {
  const StudentProfilePage({
    super.key,
  });

  @override
  State<StudentProfilePage> createState() => _StudentProfilePageState();
}

class _StudentProfilePageState extends State<StudentProfilePage> {
  String name = '';
  String email = '';

  final weightController = TextEditingController();
  final heightController = TextEditingController();
  var student = main.student;
  var heightInputMessage = "Geef je lengte in";
  var weightInputMessage = "Geef je gewicht in";
  var selectedClassGroup = main.student.classGroup;
  
  var basePadding = const EdgeInsets.all(20);



  @override //Set State for appbar
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
  void dispose() {
    weightController.dispose();
    heightController.dispose();
    super.dispose();
  }

  void setbaseHeightMessage() {
    heightInputMessage = "Geef je lengte in";
  }

  void setBaseWeightMessage() {
    weightInputMessage = "Geef je gewicht in";
  }

  @override
  Widget build(BuildContext context) {
    var appState = context.watch<main.StudentAppState>();
    appState.calculateBmi(student);

    return Scaffold(
      appBar: StudentAppBar(name: name, email: email),
      body: Center(
        child: SingleChildScrollView(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              // Row(
              //   children: [
              //     Container(
              //       padding: basePadding,
              //       child: Text("voornaam : ${student.firstName}"),
              //     )
              //   ],
              // ),
              // Row(
              //   children: [
              //     Container(
              //       padding: basePadding,
              //       child: Text("familienaam : ${student.lastName}"),
              //     )
              //   ],
              // ),
              Row(
                children:[
                  Flexible(
                    flex: 6,
                    child: Container(
                      padding: basePadding,
                      child: TextField(
                        keyboardType: TextInputType.number,
                        controller: weightController,
                        decoration: InputDecoration(
                          border: const OutlineInputBorder(),
                          hintText: weightInputMessage,
                        ),
                      ),
                    )
                  ),
                  Flexible(
                    child: Container(
                      padding: basePadding,
                      child: const Text("kg"),
                    )
                  )
                ]
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: Text("Huidig gewicht : ${student.weight} kg"),
                  )
                ],
              ),
              Row(
                children: [
                  Flexible(
                      flex: 6,
                      child: Container(
                        padding: basePadding,
                        child: TextField(
                          controller: heightController,
                          keyboardType: TextInputType.number,
                          decoration: InputDecoration(
                            border: const OutlineInputBorder(),
                            hintText: heightInputMessage
                          ),
                        ),
                      )
                    ),
                  Flexible(
                    child: Container(
                      padding: basePadding,
                      child: const Text("m"),
                    )
                  )
                ],
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: Text("Huidige lengte : ${student.height} m"),
                  )
                ],
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: Text("Huidige BMI : ${student.bmi}"),
                  )
                ],
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: const Text("Klasgroep :"),
                  ),
                  DropdownButton(
                    items: const [
                      DropdownMenuItem(value: "", child: Text("<--Selecteer een klasgroep-->")),
                      DropdownMenuItem(value: "2MG", child: Text("2MG")),
                      DropdownMenuItem(value: "1MG", child: Text("1MG")),
                      DropdownMenuItem(value: "3MG", child: Text("3MG")),
                      DropdownMenuItem(value: "4MG", child: Text("4MG")),
                    ],
                    onChanged: (value) => selectedClassGroup = value,
                    value: selectedClassGroup,
                  )
                ],
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: Text("Geboortedatum : ${student.birthDate.day}/${student.birthDate.month}/${student.birthDate.year}"),
                  )
                ],
              ),
              Row(
                children: [
                  Container(
                    padding: basePadding,
                    child: Text("Geslacht : ${main.student.gender}"),
                  )
                ],
              ),
              ElevatedButton(
                onPressed: () {
                  try {
                    if (weightController.text != "") {
                      var weight = weightController.text;
                      student.weight = double.parse(weight);
                      setBaseWeightMessage();
                    }
                  } 
                  catch (e) {
                    weightInputMessage = "Gelieve een effectief gewicht in te geven.";
                  }
                  try {
                    if (heightController.text != "") {
                      var height = heightController.text;
                      student.height = double.parse(height);
                      setbaseHeightMessage();
                    }
                  } 
                  catch (e) {
                    heightInputMessage = "Gelieve een effectieve lengte in te geven";
                  }
                  if(selectedClassGroup != ""){
                    student.classGroup = selectedClassGroup;
                  }
                  weightController.text = "";
                  heightController.text = "";
                  setState(() {});
                },
                child: const Text("Update gegevens"),
              )
            ],
          ),
        )
      ),
    );
  }
}
