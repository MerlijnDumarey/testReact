
class Student{
  String lastName;
  String firstName;
  double weight;
  double height;
  String studentEmail;
  double? bmi;
  String? classGroup;
  DateTime birthDate;
  String gender;

  Student({required this.studentEmail, required this.firstName,required this.lastName ,required this.weight, required this.height, this.bmi, required this.birthDate,required this.gender, this.classGroup = ""});
  
  factory Student.fromJson(Map<String, dynamic> json) {

    return Student(
      firstName: json['firstName'],
      lastName: json['lastName'],
      weight:json['weight'],
      height:json['height'],
      studentEmail: json['studentEmail'],
      bmi: json['bmi'],
      classGroup: json['classGroup'] ,
      birthDate:json['birthDate'] ,
      gender: json['gender']
    );
  }
  Map<String, dynamic> toJson() => {
    'firstName': firstName,
    'lastName' : lastName,
    'height': height,
    'weight' : weight,
    'studentEmail' : studentEmail,
    'bmi': bmi,
    'classGroup' : classGroup,
    'birthDate': birthDate,
    'gender' : gender
  };
}