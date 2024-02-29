import 'package:befit_flutter/data/student.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'mainmenu_page.dart';
import 'apiservice.dart';

void main() {
  runApp(
    ChangeNotifierProvider(
      create: (context) => StudentAppState(),
      child: const StudentApp(),
    ),
  );
}

Student student = Student(
  studentEmail: "john@Doe", firstName: "John",lastName: "Doe" ,weight: 65.7, height: 1.75, bmi: 0, birthDate: DateTime(2000, 2, 26), gender: "M"
);

class StudentApp extends StatelessWidget {
  const StudentApp({super.key});

  @override
  Widget build(BuildContext context) {
    final appState = context.watch<StudentAppState>();
    appState.fetchData(); //fetch data when app starts

    return MaterialApp(
      title: 'Student App',
      theme: ThemeData(
        useMaterial3: true,
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.blue,
          primary: Colors.black,
          surface: const Color.fromRGBO(128, 234, 255, 1),
          background: Colors.white,
        ),
      ),
      home: const MainMenuPage(),
    );
  }
}

class StudentAppState extends ChangeNotifier {
  String name = "${student.firstName} ${student.lastName}";
  String email = "John.Doe@gmail.com";

  final ApiService _apiService = ApiService();

  Future<void> fetchData() async {
    final data = await _apiService.fetchData();
    name = data['name'] ?? 'Unknown';
    email = data['email'] ?? 'Unknown';
    notifyListeners();
  }

  void calculateBmi(Student student) {
    double squareHeight = student.height * (student.height);
    student.bmi = student.weight / squareHeight;
  }
}
