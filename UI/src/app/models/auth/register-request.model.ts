export interface RegisterRequest{
    firstName: string,
    lastName: string,
    email: string,
    userName: string,
    phoneNumber: string,
    password: string,
    userType: number,
    recaptchaToken?: string
}