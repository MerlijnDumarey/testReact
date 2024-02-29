
import { Student, Attempt, Test, Category, Lector } from '../models/models';

export class MockDataService {

    //#region Mocking
    private students: Student[] = this.generateStudents(50);
    private attempts: Attempt[] = this.generateAttempts(100);
    private tests: Test[] = this.generateTests(30);
    private categories: Category[] = [
        { id: 'c1', name: 'Kracht' },
        { id: 'c2', name: 'Lenigheid' },
        { id: 'c3', name: 'Uithouding' },
        { id: 'c4', name: 'Snelheid' },
        { id: 'c5', name: 'Coördinatie' },
        { id: 'c6', name: 'Overig' }
    ];
    private lectors: Lector[] = this.generateLectors(20);

    private generateStudents(count: number): Student[] {
        let students: Student[] = [];
        for (let i = 1; i <= count; i++) {
            students.push({
                id: `s${i}`,
                name: `StudentVoornaam${i}`,
                familyName: `StudentAchternaam${i}`,
                email: `student${i}@school.be`,
                gender: i % 2 === 0 ? 'Mannelijk' : 'Vrouwelijk',
                currentSemester: Math.floor(Math.random() * 6) + 1,
                weight: Math.floor(Math.random() * 30) + 50,
                height: Math.floor(Math.random() * 50) + 150,
                hasEvaluationPermission: Math.random() > 0.5,
            });
        }
        return students;
    }

    private generateLectors(count: number): Lector[] {
        let lectors: Lector[] = [];
        for (let i = 1; i <= count; i++) {
            lectors.push({
                id: `l${i}`,
                name: `LectorVoornaam${i}`,
                familyName: `LectorAchternaam${i}`,
                email: `lector${i}@school.be`,
            });
        }
        return lectors;
    }

    private generateTests(count: number): Test[] {
        let tests: Test[] = [];
        for (let i = 1; i <= count; i++) {
            tests.push({
                id: `t${i}`,
                name: `TestNaam${i}`,
                description: `Dit is een beschrijving voor test ${i}`,
                unit: 'eenheid',
                comparisonType: ['0', '1', '2'][Math.floor(Math.random() * 3)] as '0' | '1' | '2',
                lowerBound: Math.floor(Math.random() * 50),
                higherBound: Math.floor(Math.random() * 50) + 50,
                categoryId: `c${Math.floor(Math.random() * 6) + 1}`,
            });
        }
        return tests;
    }

    private generateAttempts(count: number): Attempt[] {
        let attempts: Attempt[] = [];
        for (let i = 1; i <= count; i++) {
            attempts.push({
                id: `a${i}`,
                studentId: `s${Math.floor(Math.random() * 50) + 1}`,
                testId: `t${Math.floor(Math.random() * 30) + 1}`,
                score: Math.floor(Math.random() * 100),
            });
        }
        return attempts;
    }
    //#endregion

// CRUD operations

    //#region Student
    addStudent(student: Student): void {
        this.students.push(student);
    }

    getStudentById(id: string): Student | undefined {
        var student = this.students.find(s => s.id === id);
        return student;
    }

    getAllStudents(): Student[] {
        return this.students;
    }

    updateStudent(student: Student): void {
        const index = this.students.findIndex(s => s.id === student.id);
        this.students[index] = student;
    }

    deleteStudent(id: string): void {
        const index = this.students.findIndex(s => s.id === id);
        this.students.splice(index, 1);
    }

    //#endregion

    //#region Attempt
    addAttempt(attempt: Attempt): void {
        this.attempts.push(attempt);
    }

    getAttemptById(id: string): Attempt | undefined {
        return this.attempts.find(a => a.id === id);
    }

    getAllAttempts(): Attempt[] {
        return this.attempts;
    }

    updateAttempt(attempt: Attempt): void {
        const index = this.attempts.findIndex(a => a.id === attempt.id);
        this.attempts[index] = attempt;
    }

    deleteAttempt(id: string): void {
        const index = this.attempts.findIndex(a => a.id === id);
        this.attempts.splice(index, 1);
    }

    getAttemptsByStudentId(studentId: string): Attempt[] {
        return this.attempts.filter(a => a.studentId === studentId);
    }

    getAttemptsByTestId(testId: string): Attempt[] {
        return this.attempts.filter(a => a.testId === testId);
    }

    getAttemptsByStudentIdAndTestId(studentId: string, testId: string): Attempt[] {
        return this.attempts.filter(a => a.studentId === studentId && a.testId === testId);
    }
    //#endregion

    //#region Test

    addTest(test: Test): void {
        this.tests.push(test);
    }

    getTestById(id: string): Test | undefined {
        return this.tests.find(t => t.id === id);
    }

    public getAllTests(): Test[] {
        return this.tests;
    }

    updateTest(test: Test): void {
        const index = this.tests.findIndex(t => t.id === test.id);
        this.tests[index] = test;
    }

    getTestsByCategoryId(categoryId: string): Test[] {
        return this.tests.filter(t => t.categoryId === categoryId);
    }

    //#endregion

    //#region Category

    addCategory(category: Category): void {
        this.categories.push(category);
    }

    getCategoryById(id: string): Category | undefined {
        return this.categories.find(c => c.id === id);
    }

    getAllCategories(): Category[] {
        return this.categories;
    }

    updateCategory(category: Category): void {
        const index = this.categories.findIndex(c => c.id === category.id);
        this.categories[index] = category;
    }

    getCategoriesByLectorId(lectorId: string): Category[] {
        return this.categories.filter(c => c.id === lectorId);
    }

    getCategoriesByStudentId(studentId: string): Category[] {

        return this.categories.filter(c => c.id === studentId);
    }

    getCategoriesByTestId(testId: string): Category[] {

        return this.categories.filter(c => c.id === testId);
    }

    //#endregion

    //#region Lector

    addLector(lector: Lector): void {
        this.lectors.push(lector);
    }

    getLectorById(id: string): Lector | undefined {
        return this.lectors.find(l => l.id === id);
    }

    getAllLectors(): Lector[] {
        return this.lectors;
    }

    updateLector(lector: Lector): void {
        const index = this.lectors.findIndex(l => l.id === lector.id);
        this.lectors[index] = lector;
    }

    deleteLector(id: string): void {
        const index = this.lectors.findIndex(l => l.id === id);
        this.lectors.splice(index, 1);
    }
    //#endregion

}

export const Mocking = new MockDataService();