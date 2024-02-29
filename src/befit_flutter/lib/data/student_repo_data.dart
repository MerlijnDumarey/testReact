import 'package:befit_flutter/data/abstract_student_repo.dart';
import 'package:befit_flutter/data/student.dart';


class StudentRepository{
  final AbstractStudentProvider dataProvider;

  StudentRepository({required this.dataProvider});

  var studentName = "John";
  var studentEmail = "john@doe";
  var weight =50.2;
  var height = 1.65;

  Future<void> updateWeight(double newWeight){
    return dataProvider.updateWeight(newWeight);
  }

  Future<void> updateHeight(double newHeight){
    return dataProvider.updateHeight(newHeight);
  }


  Future<Student> getStudent(){
    return dataProvider.getStudent();
  }

}
