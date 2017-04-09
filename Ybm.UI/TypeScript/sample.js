var Student = (function () {
    //Constructor            
    function Student(firstName, lastName, schoolName, city, yearOfBirth) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.yearOfBirth = yearOfBirth;
        this.city = city;
        this.schoolName = schoolName;
    }
    //Function
    Student.prototype.age = function () {
        return 2014 - this.yearOfBirth;
    };
    //Function
    Student.prototype.printStudentFullName = function () {
        alert(this.lastName + ',' + this.firstName);
    };
    return Student;
}());
var Test = (function () {
    function Test() {
    }
    Test.prototype.Test = function () {
        var Id = 10;
    };
    return Test;
}());
var addFunction = function (n1, n2, n3) {
    var Id = 10;
    var sum = n1 + n2 + n3;
    return sum;
};
//# sourceMappingURL=sample.js.map