import React, { useState } from 'react';
import ẢnhLogin from '../../assets/Ảnh Login.png';
import { useNavigate } from 'react-router-dom';
import { resetPasswordByEmail } from '../../service/authService';

const ResetPasswordPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState<string | null>(null);
    const [error, setError] = useState<string | null>(null);

    const navigate = useNavigate();

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        setMessage(null);

        try {
            const response = await resetPasswordByEmail (email, newPassword);
            console.log("Reset password response:", response);
            setMessage("Đặt lại mật khẩu thành công! Vui lòng đăng nhập lại.");
            setTimeout(() => navigate('/login'), 2000); // chuyển sau 2s
        } catch (err) {
            console.error(err);
            setError("Lỗi đặt lại mật khẩu. Vui lòng kiểm tra email hoặc thử lại sau.");
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className='flex h-screen relative bg-white overflow-hidden'>
            <div className='w-1/2 bg-black'>
                <img src={ẢnhLogin} alt="Background" className='w-full h-full object-cover' />
            </div>

            <div className='w-1/2 flex items-center justify-center p-10'>
                <div className='max-w-md w-full space-y-6'>
                    <div className="flex flex-col items-center mb-10">
                        <h1 className="text-black text-6xl font-semibold">Reset Password</h1>
                    </div>

                    <form onSubmit={handleSubmit} autoComplete="off" className='w-full space-y-6'>
                        <input
                            type="email"
                            placeholder='Email'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className='w-full bg-white p-3 border border-stone-400/50 rounded-md text-stone-500 text-md font-normal'
                            required
                        />
                        <input
                            type="password"
                            placeholder='Mật khẩu mới'
                            value={newPassword}
                            onChange={(e) => setNewPassword(e.target.value)}
                            autoComplete="new-password"
                            
                            className='w-full p-3 border border-stone-400/50 rounded-md text-stone-500 text-md font-normal'
                            required
                        />
                        <button
                            type="submit"
                            className='w-full bg-blue-500 text-white p-3 rounded-md hover:bg-blue-600 transition-colors'
                            disabled={loading}
                        >
                            {loading ? 'Đang gửi...' : 'Đặt lại mật khẩu'}
                        </button>

                        {message && <p className="text-green-500 text-center">{message}</p>}
                        {error && <p className="text-red-500 text-center">{error}</p>}

                        <p className='text-center text-sm'>
                            Đã có tài khoản? <a href="/login" className='text-blue-500 hover:underline'>Đăng nhập</a>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default ResetPasswordPage;
