export interface AuthRequest{
    email: string,
    password: string,
    rememberMe?: boolean,
    recaptchaToken?: string
}