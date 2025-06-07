import React, { useState } from 'react';
import ẢnhLogin from '../../assets/Ảnh Login.png';

const LoginPage: React.FC = () => {
    const [emailOrUsername, setEmailOrUsername] = React.useState('');
    const [password, setPassword] = React.useState('');
    const [rememberMe, setRememberMe] = React.useState(false);

    const handeSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // Handle login logic here
        console.log('Email/Username:', emailOrUsername);
    };

    return (
        <div className='flex h-screen relative bg-white overflow-hidden'>
            <div className='w-1/2 bg-black'>
                <img src={ẢnhLogin} alt="Login Background" className='w-full h-full object-cover' />
            </div>

            <div className='w-1/2 flex items-center justify-center p-10'>
                <div className='max-w-md w-full space-y-6'>
                    <div className="flex flex-col items-center mb-10">
                        <h1 className="text-black text-8xl font-normal">AIDIMS</h1>
                        <p className="text-black text-4xl font-normal mt-2 ml-4">
                            AI DICOM Image<br />Management System
                        </p>
                    </div>

                    <form onSubmit={handeSubmit} className='w-full max-w-md space-y-6'>
                        <input type="text" placeholder='Email or Username' value={emailOrUsername} onChange={(e) => setEmailOrUsername(e.target.value)}
                            className='w-full  bg-white p-3 border border-stone-400/50 rounded-md justify-start text-stone-500 text-md font-normal' required />
                        <input type="password" placeholder='Password' value={password} onChange={(e) => setPassword(e.target.value)}
                            className='w-full p-3 border border-stone-400/50 rounded-md justify-start text-stone-500 text-md font-normal' required />
                        <div className='flex items-center justify-between'>
                            <label className='flex items-center'>
                                <input type="checkbox" checked={rememberMe} onChange={(e) => setRememberMe(e.target.checked)}
                                    className='mr-2' />
                                Remember me
                            </label>
                            <a href="#" className='text-blue-500 hover:underline'>Forgot Password?</a>
                        </div>
                        <button type="submit" className='w-full bg-blue-500 text-white p-3 rounded-md hover:bg-blue-600 transition-colors'>
                            Login
                        </button>
                        <p className='text-center text-sm'>
                            Don't have an account? <a href="#" className='text-blue-500 hover:underline'>Register</a>
                        </p>
                    </form>
                </div>
            </div>
        </div >
    );
}
export default LoginPage;