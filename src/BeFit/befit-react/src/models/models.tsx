// DateTime voorlopig weggelaten voor testing purposes

export  interface Student {
    id: string;
    name: string;
    familyName: string;
    email: string;
    gender: string;
    currentSemester: number;
    weight: number;
    height: number;
    hasEvaluationPermission: boolean;
}

export interface Attempt {
    id: string;
    studentId: string;
    testId: string;
    score: number;
}

export interface Test {
    id: string;
    name: string;
    description: string;
    unit: string;
    comparisonType: '0' | '1' | '2'; // H: 0, L: 1, M: 2
    lowerBound?: number;
    higherBound?: number;
    categoryId: string;
}

export interface Category {
    id: string;
    name: string;
}

export interface Lector {
    id: string;
    name: string;
    familyName: string;
    email: string;
}