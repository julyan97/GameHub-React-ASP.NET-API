export interface IContextModel
{

    id:string
    setId:any

    username: string
    setUserName: any

    setIsAuthentication: any
    isAuthenticated: boolean

    setRoles?: any,
    roles?: Array<string>,

    connection: any
}