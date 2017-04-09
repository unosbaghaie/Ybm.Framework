class Student {
    private firstName: string;
    private lastName: string;
    yearOfBirth: number;    //Public scope by default
    schoolName: string;
    city: string;
    //Constructor            
    constructor(firstName: string, lastName: string, schoolName: string, city: string, yearOfBirth: number) {

        this.firstName = firstName;
        this.lastName = lastName;
        this.yearOfBirth = yearOfBirth;
        this.city = city;
        this.schoolName = schoolName;

    }
    //Function
    age() {
        return 2014 - this.yearOfBirth;
    }
    //Function
    printStudentFullName(): void {
        alert(this.lastName + ',' + this.firstName);
    }
}



class Test
{
   
    Test()
    {
        var Id = 10;
    }

}


     var addFunction = function (n1: number, n2: number, n3: number) {

         var Id = 10;

         var sum = n1 + n2 + n3;
         return sum;
     };