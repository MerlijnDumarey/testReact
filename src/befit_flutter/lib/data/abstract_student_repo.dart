import 'package:befit_flutter/data/student.dart';

abstract class AbstractStudentProvider {
  
  Future<void> updateWeight(double newWeight);

  Future<void> updateHeight(double newHeight);

  Future<Student> getStudent();
}

     