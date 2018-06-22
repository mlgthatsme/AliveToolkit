#include "window_hooks.hpp"


#include <memory>
#include <vector>

static WNDPROC g_pOldProc = 0;
extern HMODULE gDllHandle;


LRESULT CALLBACK NewWindowProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_CREATE:
        abort();
        break;

    case WM_ERASEBKGND:
    {
        RECT rcWin;
        HDC hDC = GetDC(hwnd);
        GetClipBox((HDC)wParam, &rcWin);
        FillRect(hDC, &rcWin, GetSysColorBrush(COLOR_DESKTOP));  // hBrush can be obtained by calling GetWindowLong()
    }
    return TRUE;

    case WM_GETICON:
    case WM_MOUSEACTIVATE:
    case WM_NCLBUTTONDOWN:
    case WM_NCMOUSELEAVE:
    case WM_KILLFOCUS:
    case WM_SETFOCUS:
    case WM_ACTIVATEAPP:
    case WM_NCHITTEST:
    case WM_ACTIVATE:
    case WM_LBUTTONDOWN:
    case WM_LBUTTONUP:
    case WM_LBUTTONDBLCLK:
    case WM_NCCALCSIZE:
    case WM_MOVE:
    case WM_WINDOWPOSCHANGED:
    case WM_WINDOWPOSCHANGING:
    case WM_NCMOUSEMOVE:
    case WM_MOUSEMOVE:
        return DefWindowProc(hwnd, message, wParam, lParam);
    case WM_KEYDOWN:
    {
        return g_pOldProc(hwnd, message, wParam, lParam);
    }
    case WM_SETCURSOR:
    {
        // Set the cursor so the resize cursor or whatever doesn't "stick"
        // when we move the mouse over the game window.
        static HCURSOR cur = LoadCursor(0, IDC_ARROW);
        if (cur)
        {
            SetCursor(cur);
        }
        return DefWindowProc(hwnd, message, wParam, lParam);
    }

    case WM_PAINT:
    {
        PAINTSTRUCT ps;
        BeginPaint(hwnd, &ps);
        EndPaint(hwnd, &ps);

    }
    return FALSE;
    }

    return g_pOldProc(hwnd, message, wParam, lParam);
}

void CenterWnd(HWND wnd)
{
    RECT r, r1;
    GetWindowRect(wnd, &r);
    GetWindowRect(GetDesktopWindow(), &r1);
    MoveWindow(wnd, ((r1.right - r1.left) - (r.right - r.left)) / 2,
        ((r1.bottom - r1.top) - (r.bottom - r.top)) / 2,
        (r.right - r.left), (r.bottom - r.top), 0);
}

void SubClassWindow()
{
    HWND wnd = FindWindowA("ABE_WINCLASS", NULL);
    g_pOldProc = (WNDPROC)SetWindowLongA(wnd, GWL_WNDPROC, (LONG)NewWindowProc);

    for (int i = 0; i < 70; ++i)
    {
        ShowCursor(TRUE);
    }

    SetWindowLongA(wnd, GWL_STYLE, WS_OVERLAPPEDWINDOW | WS_VISIBLE);

    RECT rc;
    SetRect(&rc, 0, 0, 640, 460);
    AdjustWindowRectEx(&rc, WS_OVERLAPPEDWINDOW | WS_VISIBLE, TRUE, 0);
    SetWindowPos(wnd, NULL, 0, 0, rc.right - rc.left, rc.bottom - rc.top,
        SWP_SHOWWINDOW);


    ShowWindow(wnd, SW_HIDE);

    CenterWnd(wnd);

    ShowWindow(wnd, SW_SHOW);

    InvalidateRect(GetDesktopWindow(), NULL, TRUE);
}

void PatchWindowTitle()
{
    HWND wnd = FindWindowA("ABE_WINCLASS", NULL);
    if (wnd)
    {
        const int length = GetWindowTextLength(wnd) + 1;
        std::vector< char > titleBuffer(length + 1);
        if (GetWindowTextA(wnd, titleBuffer.data(), length))
        {
            std::string titleStr(titleBuffer.data());
            titleStr += " | Alive Toolkit";
            SetWindowTextA(wnd, titleStr.c_str());
        }
    }
}