import React, { useState } from 'react';
import ẢnhLogin from '../../assets/Ảnh Login.png';
import { useNavigate } from 'react-router-dom';
import { authService } from "../../service/authService";

const LoginPage: React.FC = () => {
    const [emailOrUsername, setEmailOrUsername] = useState('');
    const [password, setPassword] = useState('');

    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);

        try {
            const response = await authService.login(emailOrUsername, password);
            console.log("Login response:", response);
            const { token, role } = response;
            const normalizedRole = role.toLowerCase();

            sessionStorage.setItem('accessToken', token);
            sessionStorage.setItem('role', normalizedRole);


            if (normalizedRole === 'doctor') {
                navigate('/doctor');
            } else if (normalizedRole === 'staff') {
                navigate('/staff');
            } else {
                setError('Unknown role returned from server');
            }

        } catch (err: unknown) {
            console.error(err);
            setError('Invalid credentials or server error');
        } finally {
            setLoading(false);
        }
    };

    const handleReset = () => navigate('/resetPassword');

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

                    <form onSubmit={handleSubmit} className='w-full max-w-md space-y-6'>
                        <input
                            type="text"
                            placeholder='Email or Username'
                            value={emailOrUsername}
                            onChange={(e) => setEmailOrUsername(e.target.value)}
                            className='w-full bg-white p-3 border border-stone-400/50 rounded-md text-stone-500 text-md font-normal'
                            required
                        />
                        <input
                            type="password"
                            placeholder='Password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className='w-full p-3 border border-stone-400/50 rounded-md text-stone-500 text-md font-normal'
                            required
                        />
                        <div className='flex items-center justify-between'>
                            <label className='flex items-center'>
                               
                            </label>
                            <a href="#" className='text-blue-500 hover:underline'
                            onClick ={handleReset}
                            >Forgot Password?</a>
                        </div>
                        <button
                            type="submit"
                            className='w-full bg-[#506F9C] text-white p-3 rounded-md hover:bg-blue-700 transition-colors'
                            disabled={loading}
                        >
                            {loading ? 'Logging in...' : 'Login'}
                        </button>

                        {error && <p className="text-red-500 text-center">{error}</p>}

                    </form>
                </div>
            </div>
        </div>
    );
};

export default LoginPage;